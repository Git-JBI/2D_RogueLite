using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private HeroController _target;
    private RigidMovement _movement;
    private EnemySpriteController _spriteController;

    private IState _curState;

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

    private void Awake()
    {
        //TODO 추후 Init()쪽으로 이동
        _target = FindAnyObjectByType<HeroController>();

        _movement = GetComponent<RigidMovement>();
        _spriteController = GetComponentInChildren<EnemySpriteController>();

        //TODO Hp여기서 안가져옴
        _hp = 24.0f;
    }

    private void Start()
    {
    }

    private void Update()
    {
        _curState.OnStateUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Debug.Log("Player");
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;
        Debug.Log($"{damage}의 데미지를 입었다.");

        if (_hp <= 0)
        {
        }
        else
        {
        }
    }


}
