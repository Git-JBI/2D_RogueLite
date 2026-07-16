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
    }

    public void OnStateExit()
    {
        // 타이머 초기화
        _timer = 0;
    }

    public void OnStateUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > 0.1f)
            _controller.FinishDamageState();
    }
}
