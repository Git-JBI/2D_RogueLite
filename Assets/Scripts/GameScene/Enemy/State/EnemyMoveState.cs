public class EnemyMoveState : IState
{
    private IEnemyMovement _enemyMovement;

    public EnemyMoveState(IEnemyMovement enemy)
    {
        _enemyMovement = enemy;
    }

    public void OnStateEnter()
    {
    }

    public void OnStateExit()
    {
        _enemyMovement.Stop();
    }

    public void OnStateUpdate()
    {
        _enemyMovement.Move();
    }
}
