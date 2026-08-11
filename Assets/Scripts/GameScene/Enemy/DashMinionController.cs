using UnityEngine;

public class DashMinionController : MinionEnemyController
{
    private EnemyMoveState _moveState;
    private MinionDashState _dashState;

    protected override void Awake()
    {
        base.Awake();

        _moveState = new EnemyMoveState(this);
        _dashState = new MinionDashState(this);
    }

    public void HandleDashStarted()
    {
        // 방향 설정
        Vector2 dir = DirToTarget();

        // 속도 설정
        _rigidbody.linearVelocity = dir * _model.MoveSpeed * 5;

        // 스프라이트 설정(FlipX, 애니메이션 재생 속도 변경 2배)
        _spriteController.SetFlipX(dir.x);
        _spriteController.SetAnimSpeed(2);
    }

    public void HandleDashCompleted()
    {
        ChangeState(_moveState);

        // 스프라이트 설정(애니메이션 재생 속도 원상 복구)
        _spriteController.SetAnimSpeed(1);
    }

    public override void HandleDamagedCompleted()
    {
        ChangeState(_moveState);
    }

    public override void HandleMoveCompleted()
    {
        ChangeState(_dashState);
    }

    public override void Move()
    {
        // 방향 설정
        Vector2 dir = DirToTarget();

        // 속도 설정
        _rigidbody.linearVelocity = dir * _model.MoveSpeed;

        // 스프라이트 설정
        _spriteController.SetFlipX(dir.x);
    }

    protected override void SetStartState()
    {
        _curState = _moveState;
    }
}
