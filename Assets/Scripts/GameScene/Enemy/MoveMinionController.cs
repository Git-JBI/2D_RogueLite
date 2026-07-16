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

    public override void FinishDamageState()
    {
        ChangeState(_moveState);
    }
}