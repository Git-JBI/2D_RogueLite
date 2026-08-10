using UnityEngine;

public class MinionDashState : IState
{
    private DashMinionController _controller;

    private float _timer;

    public MinionDashState(DashMinionController controller)
    {
        _controller = controller;

        // 타이머 초기화
        _timer = 0;
    }

    public void OnStateEnter()
    {
        // 타이머 초기화
        _timer = 0;

        _controller.HandleDashStarted();
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > 0.5f)
            _controller.HandleDashCompleted();
    }
}
