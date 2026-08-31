public class ChaseMinionNormalState : IState
{
    private ChaseMinionController _controller;

    public ChaseMinionNormalState(ChaseMinionController enemy)
    {
        _controller = enemy;
    }

    public void OnStateEnter()
    {
    }

    public void OnStateExit()
    {
        _controller.Stop();
    }

    public void OnStateUpdate()
    {
        _controller.SetVelocityToTarget();
    }
}