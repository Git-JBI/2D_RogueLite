using UnityEngine;

public abstract class MoveEnemyController : EnemyControllerBase
{
    protected RigidMovement _movement;

    protected EnemyMoveState _moveState;

    public void Move()
    {
        _movement.Move(DirToTarget(), _model.MoveSpeed);
        _spriteController.SetFlipX(DirToTarget().x);
    }
    public void Stop()
    {
        _movement.Move(Vector2.zero, 0);
    }
}
