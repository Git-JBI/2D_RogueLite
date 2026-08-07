using UnityEngine;

public class MoveMinionController : MinionEnemyController
{
    private EnemyMoveState _moveState;

    protected override void Awake()
    {
        base.Awake();

        _moveState = new EnemyMoveState(this);
    }

    public override void Move()
    {
        // 방향 설정
        Vector2 dir = DirToTarget();

        // 속도 설정(방향 * 속력)
        _rigidbody.linearVelocity = dir * _model.MoveSpeed;
        _spriteController.SetFlipX(dir.x);
    }
    public override void HandleMoveCompleted()
    {
        // 정지
        _rigidbody.linearVelocity = Vector2.zero;
    }
    public override void HandleDamagedCompleted()
    {
        ChangeState(_moveState);
    }

    protected override void Damaged()
    {
        base.Damaged();

        ChangeState(_damagedState);
    }
    protected override void SetStartState()
    {
        _curState = _moveState;
    }
}