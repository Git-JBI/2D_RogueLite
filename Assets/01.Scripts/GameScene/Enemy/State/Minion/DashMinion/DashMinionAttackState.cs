using UnityEngine;

public class DashMinionAttackState : IState
{
    private DashMinionController _controller;

    private float _timer;
    private float _updateTime;

    /// <summary>
    /// DashMinionAttackState 생성자
    /// </summary>
    /// <param name="controller">컨트롤러</param>
    /// <param name="updateTime">Update진행할 시간</param>
    public DashMinionAttackState(DashMinionController controller, float updateTime)
    {
        _controller = controller;
        _updateTime = updateTime;
    }

    public void OnStateEnter()
    {
        // 타이머 초기화
        _timer = 0;

        // 대시 속도 설정
        _controller.SetDashSpeed();

        // 애니메이션 재생 속도 변경
        _controller.SetAnimSpeed(2);
    }

    public void OnStateExit()
    {
        // 애니메이션 재생 속도 원상복구
        _controller.SetAnimSpeed(1);
    }

    public void OnStateUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > _updateTime)
            _controller.ChangeState(_controller.NormalState);
    }
}
