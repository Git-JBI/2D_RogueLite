using UnityEngine;

public class EnemyDeadState : IState
{
    private EnemyControllerBase _enemy;

    private float _timer;

    public EnemyDeadState(EnemyControllerBase enemy)
    {
        _enemy = enemy;
    }

    public void OnStateEnter()
    {
        // 죽음 상태 시작
        _enemy.EnterDeadState();
    }

    public void OnStateExit()
    {
        // 타이머 초기화
        _timer = 0;

        // 죽음 상태 끝
        _enemy.ExitDeadState();
    }

    public void OnStateUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > 0.5f)
            OnStateExit();
    }
}
