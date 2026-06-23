using System.Collections;
using UnityEngine;

public class EnemyDamagedState : IState
{
    private EnemyController _enemy;

    private float _timer;

    public EnemyDamagedState(EnemyController enemy)
    {
        _enemy = enemy;
    }

    public void OnStateEnter()
    {
        _enemy.SpriteController.Damaged();
    }

    public void OnStateExit()
    {
        _timer = 0.0f;
    }

    public void OnStateUpdate()
    {
        if (_timer < 0.2f)
            _timer += Time.deltaTime;
        else
            _enemy.ChangeState(_enemy.MoveState);
    }
}
