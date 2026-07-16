using UnityEngine;

[System.Serializable]
public abstract class EnemyControllerBase : MonoBehaviour, IDamageable
{
    protected Transform _heroTransform;
    protected EnemyModel _model;
    protected EnemySpriteController _spriteController;
    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;

    protected IState _curState;
    
    protected EnemyDeadState _deadState;

    public EnemyModel Model => _model;
    public Rigidbody2D Rigidbody => _rigidbody;

    protected virtual void Awake()
    {
        _spriteController = GetComponentInChildren<EnemySpriteController>();
        _rigidbody = GetComponentInChildren<Rigidbody2D>();
        _collider = GetComponentInChildren<Collider2D>();

        _deadState = new EnemyDeadState(this);
    }

    private void Update()
    {
        if (_curState != null)
            _curState.OnStateUpdate();
    }
    
    /// <summary>
    /// 적 초기화 함수(적마다 각 최초 상태 설정, model 생성 필요)
    /// </summary>
    /// <param name="target"></param>
    /// <param name="spawnPos"></param>
    public virtual void Init(Transform target, Vector2 spawnPos)
    {
        // 타겟 설정(플레이어)
        _heroTransform = target;

        // 생성 위치 설정
        transform.position = spawnPos;
    }

    public void ChangeState(IState state)
    {
        if (_curState == _deadState)
            return;

        if (_curState != null)
            _curState.OnStateExit();

        _curState = state;

        _curState.OnStateEnter();
    }

    /// <summary>
    /// 플레이어쪽 방향을 구하는함수
    /// </summary>
    /// <returns>플레이어 방향 vector2</returns>
    public Vector2 DirToTarget()
    {
        return (_heroTransform.position - transform.position).normalized;
    }

    /// <summary>
    /// 콜라이더 활성화 여부 설정 함수
    /// </summary>
    /// <param name="x"></param>
    public void SetColliderActive(bool x)
    {
        _collider.enabled = x;
    }
    /// <summary>
    /// 리지드바디 활성화 여부 설정 함수
    /// </summary>
    /// <param name="x"></param>
    public void SetRigidBodyActive(bool x)
    {
        _rigidbody.simulated = x;
    }
    
    public void EnterDamaged()
    {
        _spriteController.Damaged();
    }
    public void EnterDead()
    {
        // 리지드바디 비활성화
        SetRigidBodyActive(false);

        // 콜라이더 비활성화
        SetColliderActive(false);

        // 죽음 애니메이션 트리거실행
        _spriteController.Dead();
    }
    public virtual void Dead()
    {
        // 현재 상태 비우기
        _curState = null;
    }

    protected virtual void Damaged()
    {
        _spriteController.Damaged();
    }

    //TODO 계속 플레이어와 맞닿아 있는 경우 데미지 <= OnCollisionEnter2D, Stay2D 2종류 다 써야 할 듯 attackTimer 추가
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable player = collision.gameObject.GetComponent<IDamageable>();
            player.TakeDamage(_model.Damage);
        }
    }

    #region Interface
    public void TakeDamage(float damage)
    {
        _model.TakeDamage(damage);

        if (_model.CurHp <= 0)
        {
            ChangeState(_deadState);
        }
        else
        {
            Damaged();
        }
    }
    #endregion
}