using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour, IDamageable
{
    private HeroController _target;
    private EnemyModel _model;
    private RigidMovement _movement;
    private Rigidbody2D _rigidbody;
    private EnemySpriteController _spriteController;
    private Collider2D _collider;

    private IState _curState;

    public event UnityAction OnDead;

    public HeroController Target => _target;
    public EnemyModel Model => _model;
    public RigidMovement Movement => _movement;
    public Rigidbody2D Rigidbody => _rigidbody;
    public EnemySpriteController SpriteController => _spriteController;
    public Collider2D Collider => _collider;


    private EnemyMoveState _moveState;
    private EnemyDamagedState _damagedState;
    private EnemyDeadState _deadState;

    public EnemyMoveState MoveState => _moveState;
    public EnemyDamagedState DamagedState => _damagedState;
    public EnemyDeadState DeadState => _deadState;

    private void Awake()
    {
        //TODO 추후 Init()쪽으로 이동
        _target = FindAnyObjectByType<HeroController>();

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
    
    public void Init(Vector2 spawnPos)
    {
        ChangeState(_moveState);
        _movement.SetActive(true);
        SetColliderActive(true);
        transform.position = spawnPos;
        _model.Init();
    }

    public void ChangeState(IState state)
    {
        if (_curState != null)
            _curState.OnStateExit();

        _curState = state;

        _curState.OnStateEnter();
    }

    public void SetColliderActive(bool x)
    {
        _collider.enabled = x;
    }

    public void Die()
    {
        // 죽음 이벤트 실행
        OnDead?.Invoke();

        // 현재 상태 비우기
        _curState = null;

        // 이벤트 비우기
        OnDead = null;

        SpawnExpItem();

        //TODO 추후 Util사용해서 더 확장필요
        PoolingObject poolOb = GetComponent<PoolingObject>();
        poolOb.ReturnToPool();
        // Destroy(gameObject);
    }

    private void SpawnExpItem()
    {
        GameObject expItem;

        expItem = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Item/Item_Exp", transform);

        ExpItem item = expItem.GetComponent<ExpItem>();

        item.Init(transform.position);
    }

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
