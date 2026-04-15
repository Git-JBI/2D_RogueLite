using System.Collections;
using UnityEngine;

public class EnemyMoveState : MonoBehaviour, IState
{
    private EnemySpriteController _spriteController;
    private RigidMovement _movement;
    private HeroController _target;
    private EnemyController _controller;

    private bool _isMoving;

    private Coroutine _moveCoroutine;

    public void Init(EnemySpriteController spriteController, RigidMovement movement, HeroController target, EnemyController enemyController)
    {
        _spriteController = spriteController;
        _movement = movement;
        _target = target;
        _controller = enemyController;
    }

    private Vector2 DirToTarget()
    {
        return (_target.transform.position - _controller.transform.position).normalized;
    }

    private IEnumerator MoveCoroutine()
    {
        while (_isMoving)
        {
            _movement.Move(DirToTarget(), 1.0f);
            _spriteController.SetFlipX(DirToTarget().x);
            yield return null;
        }
    }

    public void OnStateEnter()
    {
        Debug.Log("MoveEnter");
        _isMoving = true;
        _moveCoroutine = StartCoroutine(MoveCoroutine());
    }

    public void OnStateExit()
    {
        Debug.Log("MoveExit");
        _isMoving = false;
        StopCoroutine(_moveCoroutine);
        _movement.Move(Vector2.zero, 0.0f);
    }

    public void OnStateUpdate()
    {
        
    }
}
