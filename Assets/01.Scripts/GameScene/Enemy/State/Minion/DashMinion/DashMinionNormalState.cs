using UnityEngine;

public class DashMinionNormalState : IState
{
    private DashMinionController _controller;

    private float _timer;
    private float _updateTime;

    public DashMinionNormalState(DashMinionController controller, float updateTime)
    {
        _controller = controller;
        _updateTime = updateTime;
    }

    public void OnStateEnter()
    {
        // 타이머 초기화
        _timer = 0.0f;
    }

    public void OnStateExit()
    {
        _controller.Stop();
    }

    public void OnStateUpdate()
    {
        // 속도 설정
        _controller.SetVelocityToTarget();

        _timer += Time.deltaTime;

        if (_timer >= _updateTime)
            _controller.ChangeState(_controller.AttackState);
    }
}
