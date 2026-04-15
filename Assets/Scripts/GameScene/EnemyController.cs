using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private HeroController _target;
    private RigidMovement _movement;
    private EnemySpriteController _spriteController;

    private Vector2 _dirToTarget;

    private void Awake()
    {
        //TODO 추후 Init()쪽으로 이동
        _target = FindAnyObjectByType<HeroController>();

        _movement = GetComponent<RigidMovement>();
        _spriteController = GetComponentInChildren<EnemySpriteController>();
    }

    private void Update()
    {
        _movement.Move(DirToTarget(), 1.0f);
        _spriteController.SetFlipX(DirToTarget().x);
    }

    private Vector2 DirToTarget()
    {
        return (_target.transform.position - transform.position).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Debug.Log("Player");
    }

    public void Damaged(float damage)
    {
        _spriteController.Damaged();
        Debug.Log($"{damage}의 데미지를 입었다.");
    }
}
