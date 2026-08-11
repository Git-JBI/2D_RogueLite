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

    protected virtual void Awake()
    {
        _spriteController = GetComponentInChildren<EnemySpriteController>();
        _rigidbody = GetComponentInChildren<Rigidbody2D>();
        _collider = GetComponentInChildren<Collider2D>();

        _deadState = new EnemyDeadState(this);
    }

    private void Update()
    {
        _curState?.OnStateUpdate();
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
    /// <summary>
    /// 죽음 트리거 실행 함수
    /// </summary>
    public void DeadTrigger()
    {
        _spriteController.Dead();
    }
    /// <summary>
    /// 상태를 바꾸는 함수
    /// </summary>
    /// <param name="state">바꿀 상태</param>
    protected void ChangeState(IState state)
    {
        // 현 상태가 죽음 상태라면 어느 상태로든 변하지 않음
        if (_curState == _deadState)
            return;

        // 현재 상태 나감 실행
        _curState?.OnStateExit();

        // 상태 변경
        _curState = state;

        // 바뀐 상태 OnStateEnter() 실행
        _curState.OnStateEnter();
    }

    protected virtual void Damaged()
    {
        _spriteController.Damaged();
    }

    public abstract void Move();
    public abstract void HandleMoveCompleted();
    public abstract void HandleDeadCompleted();
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