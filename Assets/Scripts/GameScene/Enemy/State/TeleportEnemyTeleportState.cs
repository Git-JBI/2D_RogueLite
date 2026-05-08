using UnityEngine;

public class TeleportEnemyTeleportState : IState
{
    private TeleportEnemyController _controller;

    public TeleportEnemyTeleportState(TeleportEnemyController controller)
    {
        _controller = controller;
    }

    public void OnStateEnter()
    {
        _controller.StartTeleport();
    }

    public void OnStateExit()
    {
        _controller.ShootSpiral8Directions();
        _controller.EndTeleport();
    }

    public void OnStateUpdate()
    {
    }
}
