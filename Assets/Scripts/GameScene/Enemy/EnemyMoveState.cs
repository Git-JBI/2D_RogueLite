using System.Collections;
using UnityEngine;

public class EnemyMoveState : IState
{
    private EnemyController _enemy;

    public EnemyMoveState(EnemyController enemy)
    {
        _enemy = enemy;
    }

    private Vector2 DirToTarget()
    {
        return (_enemy.Target.transform.position - _enemy.transform.position).normalized;
    }

    public void OnStateEnter()
    {
    }

    public void OnStateExit()
    {
        _enemy.Movement.Move(Vector2.zero, 0.0f);
    }

    public void OnStateUpdate()
    {
        _enemy.Movement.Move(DirToTarget(), _enemy.Model.MoveSpeed);
        _enemy.SpriteController.SetFlipX(DirToTarget().x);
    }
}
