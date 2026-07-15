public class EnemyMoveState : IState
{
    private MoveEnemyController _enemy;

    public EnemyMoveState(MoveEnemyController enemy)
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
