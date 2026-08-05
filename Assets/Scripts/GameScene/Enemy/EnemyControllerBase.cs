using UnityEngine;

[System.Serializable]
public abstract class EnemyControllerBase : MonoBehaviour, IDamageable
{
    protected Transform _target;
    protected EnemyModel _model;
    protected EnemySpriteController _spriteController;
    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;

    protected IState[] _states;
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

    protected void ChangeState(IState state)
    {
        // 현 상태가 죽음 상태라면 어느 상태로든 변하지 않음
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
        return (_target.position - transform.position).normalized;
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
    public abstract void Dead();
    protected virtual void Damaged()
    {
        _spriteController.Damaged();
    }
    protected abstract void SetStartState();

    /// <summary>
    /// 플레이어 위치 기준으로 스폰할 랜덤한 위치를 반환하는 함수
    /// </summary>
    /// <returns></returns>
    protected Vector2 RandomSpawnPosition()
    {
        Vector2 pos = _target.position;

        pos += Random.insideUnitCircle.normalized * 10.0f;

        return pos;
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