using UnityEngine;

public class TeleportBossIdleState : IState
{
    private TeleportBossController _controller;

    private float _timer;
    private bool _isAttackTime;

    public TeleportBossIdleState(TeleportBossController controller)
    {
        _controller = controller;
    }

    public void OnStateEnter()
    {
        _timer = 0;
        _isAttackTime = false;
    }

    public void OnStateExit()
    {
        
    }

    public void OnStateUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= 2.5f && !_isAttackTime)
        {
            _controller.Shoot8Directions();
            _isAttackTime = true;
        }
        
        if(_timer >= 5.0f)
        {
            _controller.FinishIdle();
        }
    }
}
