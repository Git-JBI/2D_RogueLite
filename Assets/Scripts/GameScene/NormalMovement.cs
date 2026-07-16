using UnityEngine;

public class NormalMovement : IEnemyMovement
{
    private EnemySpriteController _spriteController;
    private EnemyControllerBase _controller;
    public NormalMovement(EnemyControllerBase controller, EnemySpriteController spriteController)
    {
        Init(controller);
        _spriteController = spriteController;
    }

    public void Init(EnemyControllerBase controller)
    {
        _controller = controller;
    }

    public void Move()
    {
        Vector2 dir = _controller.DirToTarget();
        _spriteController.SetFlipX(dir.x);
        _controller.Rigidbody.linearVelocity = dir * _controller.Model.MoveSpeed;
    }

    public void Stop()
    {
        _controller.Rigidbody.linearVelocity = Vector2.zero;
    }
}