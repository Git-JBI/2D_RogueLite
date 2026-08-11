using UnityEngine;

public class TeleportBossTeleportState : IState
{
    private TeleportBossController _controller;

    public TeleportBossTeleportState(TeleportBossController controller)
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
