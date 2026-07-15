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
        _enemy.EnterDead();
    }

    public void OnStateExit()
    {
        // 타이머 초기화
        _timer = 0;

        _enemy.Dead();
    }

    public void OnStateUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > 0.5f)
            OnStateExit();
    }
}
