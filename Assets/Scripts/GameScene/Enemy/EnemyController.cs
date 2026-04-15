using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private HeroController _target;
    private RigidMovement _movement;
    private EnemySpriteController _spriteController;

    private IState _curState;

    private EnemyMoveState _moveState;
    private EnemyDamagedState _damagedState;
    private EnemyDeadState _deadState;

    private float _hp;

    private Vector2 _dirToTarget;

    public IState CurState
    {
        get
        {
            return _curState;
        }
        set
        {
            if (_curState != null)
                _curState.OnStateExit();

            _curState = value;

            _curState.OnStateEnter();
        }
    }

    public EnemyMoveState MoveState => _moveState;

    private void Awake()
    {
        //TODO 추후 Init()쪽으로 이동
        _target = FindAnyObjectByType<HeroController>();

        _movement = GetComponent<RigidMovement>();
        _spriteController = GetComponentInChildren<EnemySpriteController>();

        _moveState = GetComponent<EnemyMoveState>();
        _damagedState = GetComponent<EnemyDamagedState>();
        _deadState = GetComponent<EnemyDeadState>();

        //TODO Hp여기서 안가져옴
        _hp = 24.0f;

        _moveState.Init(_spriteController, _movement, _target, this);
        _damagedState.Init(this, _spriteController);

        CurState = _moveState;
    }

    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Debug.Log("Player");
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            //TODO 사망상태로 변화
            CurState = _deadState;
            _spriteController.Dead();
            Debug.Log("사망");
        }
        else
        {
            //TODO 경직 필요, Damaged상태로 변화
            CurState = _damagedState;
            Debug.Log($"{damage}의 데미지를 입었다.");
        }
    }
}
