using UnityEngine;

public class EnemyDamagedState : IState
{
    private EnemyControllerBase _enemy;

    private float _timer;

    public EnemyDamagedState(EnemyControllerBase enemy)
    {
        _enemy = enemy;
    }

    public void OnStateEnter()
    {
        _enemy.EnterDamaged();
    }

    public void OnStateExit()
    {
        // 타이머 초기화
        _timer = 0.0f;
    }

    public void OnStateUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > 0.2f)
            _enemy.ExitDamaged();
    }
}
