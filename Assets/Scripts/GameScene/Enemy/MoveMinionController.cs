public class MoveMinionController : MinionEnemyController
{
    private NormalMovement _movement;

    private EnemyMoveState _moveState;

    protected override void Awake()
    {
        base.Awake();

        _movement = new NormalMovement(this, _spriteController);

        _moveState = new EnemyMoveState(_movement);
    }

    protected override void SetStartState()
    {
        _curState = _moveState;
    }

    public override void ExitDamagedState()
    {
        ChangeState(_moveState);
    }

    public override void EnterDamagedState()
    {
        throw new System.NotImplementedException();
    }

    public override void EnterDeadState()
    {
        throw new System.NotImplementedException();
    }
}