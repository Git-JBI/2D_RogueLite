using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnemyController : MonoBehaviour, IDamageable
{
    private Transform _heroTransform;
    private EnemyModel _model;
    private RigidMovement _movement;
    private Rigidbody2D _rigidbody;
    private EnemySpriteController _spriteController;
    private Collider2D _collider;

    private IState _curState;

    public event UnityAction<EnemyController> OnDead;

    public EnemySpriteController SpriteController => _spriteController;

    private EnemyMoveState _moveState;
    private EnemyDamagedState _damagedState;
    private EnemyDeadState _deadState;

    public EnemyMoveState MoveState => _moveState;
    public EnemyDamagedState DamagedState => _damagedState;
    public EnemyDeadState DeadState => _deadState;

    private void Awake()
    {
        _model = GetComponent<EnemyModel>();
        _movement = GetComponent<RigidMovement>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _spriteController = GetComponentInChildren<EnemySpriteController>();
        _collider = GetComponentInChildren<Collider2D>();

        _moveState = new EnemyMoveState(this);
        _damagedState = new EnemyDamagedState(this);
        _deadState = new EnemyDeadState(this);
    }

    private void Start()
    {
        ChangeState(_moveState);
    }

    private void Update()
    {
        _curState.OnStateUpdate();
    }
    
    public void Init(Transform target, Vector2 spawnPos)
    {
        _heroTransform = target;

        ChangeState(_moveState);
        _movement.SetActive(true);
        SetColliderActive(true);
        transform.position = spawnPos;
        _model.Init();
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
    /// 플레이어의 방향을 구하는함수
    /// </summary>
    /// <returns>플레이어 방향 vector2</returns>
    public Vector2 DirToTarget()
    {
        return (_heroTransform.position - transform.position).normalized;
    }

    /// <summary>
    /// 리지드바디 상태 설정 함수
    /// </summary>
    /// <param name="value"></param>
    public void SetRigidbodyActive(bool value)
    {
        _rigidbody.simulated = value;
    }

    /// <summary>
    /// 콜라이더 상태 설정 함수
    /// </summary>
    /// <param name="x"></param>
    public void SetColliderActive(bool x)
    {
        _collider.enabled = x;
    }

    public void Move()
    {
        _movement.Move(DirToTarget(), _model.MoveSpeed);
        _spriteController.SetFlipX(DirToTarget().x);
    }
    public void Stop()
    {
        _movement.Move(Vector2.zero, 0);
    }
    public void Dead()
    {
        // 죽음 이벤트 실행
        OnDead?.Invoke(this);

        // 현재 상태 비우기
        _curState = null;

        // 이벤트 비우기
        OnDead = null;

        // 아이템 스폰
        SpawnExpItem();

        //TODO 추후 Util사용해서 더 확장필요
        PoolingObject poolOb = GetComponent<PoolingObject>();
        poolOb.ReturnToPool();
    }

    //TODO 추후 expitem의 종류 다양
    private void SpawnExpItem()
    {
        GameObject expItem;

        expItem = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Item/Item_Exp", transform);

        ExpItem item = expItem.GetComponent<ExpItem>();

        item.Init(transform.position);
    }

    //TODO 계속 플레이어와 맞닿아 있는 경우 데미지
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable player = collision.gameObject.GetComponent<IDamageable>();
            player.TakeDamage(_model.Damage);
        }
    }

    public void TakeDamage(float damage)
    {
        _model.TakeDamage(damage);

        if (_model.CurHp <= 0)
        {
            ChangeState(_deadState);
        }
        else
        {
            ChangeState(_damagedState);
        }
    }
}