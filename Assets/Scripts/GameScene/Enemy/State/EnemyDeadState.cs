using UnityEngine;

public class EnemyDeadState : IState
{
    private EnemyController _enemy;

    private float _timer;

    public EnemyDeadState(EnemyController enemy)
    {
        _enemy = enemy;
    }

    public void OnStateEnter()
    {
        // 타이머 초기화
        _timer = 0;

        // 죽음 상태 시작
        _enemy.EnterDead();
    }

    public void OnStateExit()
    {
        _enemy.Dead();
    }

    public void OnStateUpdate()
    {
        if(_timer < 0.5f)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            OnStateExit();
        }
    }
}
