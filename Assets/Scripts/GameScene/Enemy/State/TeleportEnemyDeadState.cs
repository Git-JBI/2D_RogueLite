using UnityEngine;

public class TeleportEnemyDeadState : IState
{
    private TeleportEnemyController _controller;

    public TeleportEnemyDeadState(TeleportEnemyController controller)
    {
        _controller = controller;
    }

    public void OnStateEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnStateExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnStateUpdate()
    {
        throw new System.NotImplementedException();
    }
}