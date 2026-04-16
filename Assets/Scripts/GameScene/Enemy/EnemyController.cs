using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private HeroController _target;
    private RigidMovement _movement;
    private EnemySpriteController _spriteController;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    public HeroController Target => _target;
    public RigidMovement Movement => _movement;
    public EnemySpriteController SpriteController => _spriteController;
    public Rigidbody2D Rigidbody => _rigidbody;
    public Collider2D Collider => _collider;

    private IState _curState;

    private EnemyMoveState _moveState;
    private EnemyDamagedState _damagedState;
    private EnemyDeadState _deadState;

    public EnemyMoveState MoveState => _moveState;
    public EnemyDamagedState DamagedState => _damagedState;
    public EnemyDeadState DeadState => _deadState;

    private float _hp;

    private Vector2 _dirToTarget;

    private void Awake()
    {
        //TODO 추후 Init()쪽으로 이동
        _target = FindAnyObjectByType<HeroController>();

        _movement = GetComponent<RigidMovement>();
        _spriteController = GetComponentInChildren<EnemySpriteController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponentInChildren<Collider2D>();

        //TODO Hp여기서 안가져옴
        _hp = 24.0f;

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
    
    public void ChangeState(IState state)
    {
        if (_curState != null)
            _curState.OnStateExit();

        _curState = state;

        _curState.OnStateEnter();
    }

    public void SetRigidbodyActive(bool a)
    {
        _rigidbody.simulated = a;
    }

    public void SetColliderActive(bool a)
    {
        _collider.enabled = a;
    }

    public void Die()
    {
        //TODO 추후 ObjectPooling으로 변환 시 수정 필요
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Debug.Log("Player");
    }

    public void TakeDamage(float damage)
    {
        //TODO Model사용
        _hp -= damage;
        Debug.Log($"{damage}의 데미지를 입었다.");

        if (_hp <= 0)
        {
            ChangeState(_deadState);
        }
        else
        {
            ChangeState(_damagedState);
        }
    }
}
