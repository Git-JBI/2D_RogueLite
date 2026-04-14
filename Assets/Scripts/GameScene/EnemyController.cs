using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private HeroController _target;
    private RigidMovement _movement;

    private Vector2 _dirToTarget;

    private void Awake()
    {
        _target = FindAnyObjectByType<HeroController>();
        _movement = GetComponent<RigidMovement>();
    }

    private void Update()
    {
        _movement.Move(DirToTarget(), 1.0f);
    }

    private Vector2 DirToTarget()
    {
        return (_target.transform.position - transform.position).normalized;
    }


}
