using UnityEngine;

[System.Serializable]
public abstract class EnemyControllerBase : MonoBehaviour, IDamageable
{
    protected Transform _target;
    protected EnemyModel _model;
    protected EnemySpriteController _spriteController;
    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;

    protected IState _curState;

    protected IState _normalState;
    protected IState _deadState;

    public IState NormalState => _normalState;
    public IState DeadState => _deadState;

    protected virtual void Awake()
    {
        _spriteController = GetComponentInChildren<EnemySpriteController>();
        _rigidbody = GetComponentInChildren<Rigidbody2D>();
        _collider = GetComponentInChildren<Collider2D>();
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
    /// 속도를 플레이어 방향으로 설정하는 함수
    /// </summary>
    public void SetVelocityToTarget()
    {
        // 방향 설정
        Vector2 dir = DirToTarget();

        // 속도 설정(방향 * 속력)
        _rigidbody.linearVelocity = dir * _model.MoveSpeed;

        // 스프라이트 설정
        _spriteController.SetFlipX(dir.x);
    }
    /// <summary>
    /// 속도를 0으로 설정하는 함수
    /// </summary>
    public void Stop()
    {
        _rigidbody.linearVelocity = Vector2.zero;
    }
    /// <summary>
    /// 애니메이션 재생 속도를 설정하는 함수
    /// </summary>
    /// <param name="speed">재생 속도</param>
    public void SetAnimSpeed(float speed)
    {
        _spriteController.SetAnimSpeed(speed);
    }
    /// <summary>
    /// 죽음 트리거 실행 함수
    /// </summary>
    public void DeadAnimTrigger()
    {
        _spriteController.Dead();
    }
    /// <summary>
    /// 상태를 바꾸는 함수
    /// </summary>
    /// <param name="state">바꿀 상태</param>
    public void ChangeState(IState state)
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

    /// <summary>
    /// 처음 상태 설정 함수
    /// </summary>
    protected void SetStartState()
    {
        _curState = _normalState;

        _curState.OnStateEnter();
    }

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

    /// <summary>
    /// 데미지 입었을 시 실행할 함수
    /// </summary>
    protected virtual void Damaged()
    {
        _spriteController.Damaged();
    }

    /// <summary>
    /// 적이 죽을 시 실행 될 함수
    /// </summary>
    public abstract void Remove();

    //TODO 계속 플레이어와 맞닿아 있는 경우 데미지 <= OnCollisionEnter2D, Stay2D 2종류 다 써야 할 듯 attackTimer 추가
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable player = collision.gameObject.GetComponent<IDamageable>();
            player.TakeDamage(_model.Damage, false);
        }
    }

    #region Interface
    public void TakeDamage(float damage, bool isCritical)
    {
        GameObject floater = GameManager.Instance.PoolManager.GetFromPool("Prefabs/DamageFloater/DamageFloater", null);
        DamageFloater damageFloater = floater.GetComponent<DamageFloater>();

        damageFloater.transform.position = new Vector3(
            _collider.bounds.min.x - 0.1f,
            _collider.bounds.max.y + 0.2f,
            transform.position.z);

        damageFloater.InitDamageFloater((int)damage, isCritical);

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