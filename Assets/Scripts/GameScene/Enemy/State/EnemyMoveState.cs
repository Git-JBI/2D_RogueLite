using UnityEngine;

public class EnemyMoveState : IState
{
    private EnemyController _enemy;

    public EnemyMoveState(EnemyController enemy)
    {
        _enemy = enemy;
    }

    public void OnStateEnter()
    {
    }

    public void OnStateExit()
    {
        _enemy.Stop();
    }

    public void OnStateUpdate()
    {
        _enemy.Move();
    }
}
