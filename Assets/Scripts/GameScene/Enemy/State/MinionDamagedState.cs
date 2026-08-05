using UnityEngine;

public class MinionDamagedState : IState
{
    private MinionEnemyController _controller;

    private float _timer;

    public MinionDamagedState(MinionEnemyController controller)
    {
        _controller = controller;
    }

    public void OnStateEnter()
    {
        // 타이머 초기화
        _timer = 0;

        _controller.EnterDamagedState();
    }

    public void OnStateExit()
    {
        _controller.ExitDamagedState();
    }

    public void OnStateUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > 0.1f)
            OnStateExit();
    }
}
