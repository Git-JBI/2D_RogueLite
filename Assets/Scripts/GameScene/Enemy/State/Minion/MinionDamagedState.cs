using UnityEngine;

public class MinionDamagedState : IState
{
    private MinionController _controller;

    private float _timer;
    private float _updateTime;

    public MinionDamagedState(MinionController controller, float time)
    {
        _controller = controller;

        // 업데이트 진행 시간
        _timer = time;
    }

    public void OnStateEnter()
    {
        // 타이머 초기화
        _timer = 0;
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > _updateTime)
            _controller.ChangeState(_controller.NormalState);
    }
}