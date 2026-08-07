public class EnemyMoveState : IState
{
    private EnemyControllerBase _controller;

    public EnemyMoveState(EnemyControllerBase enemy)
    {
        _controller = enemy;
    }

    public void OnStateEnter()
    {
    }

    public void OnStateExit()
    {
        _controller.HandleMoveCompleted();
    }

    public void OnStateUpdate()
    {
        _controller.Move();
    }
}