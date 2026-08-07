using UnityEngine;

public class MinionDamagedState : IState
{
    private MinionEnemyController _controller;

    /// <summary>
    /// 경직용도 타이머
    /// </summary>
    private float _timer;

    public MinionDamagedState(MinionEnemyController controller)
    {
        _controller = controller;

        // 타이머 초기화
        _timer = 0;
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

        if (_timer > 0.1f)
        {
            _controller.HandleDamagedCompleted();
        }
    }
}