using UnityEngine;

public class TeleportEnemyTeleportState : IState
{
    private TeleportBossController _controller;

    public TeleportEnemyTeleportState(TeleportBossController controller)
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
