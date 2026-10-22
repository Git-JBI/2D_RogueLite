using UnityEngine;

public class TeleportBossNormalState : IState
{
    private TeleportBossController _controller;

    private bool _isAttack;
    private float _timer;
    private float _attackDelay;
    private float _stateDuration;

    public TeleportBossNormalState(TeleportBossController controller, float attackTime, float updateTime)
    {
        _controller = controller;
        _attackDelay = attackTime;
        _stateDuration = updateTime;
    }

    public void OnStateEnter()
    {
        _timer = 0;
        _isAttack = false;
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= _attackDelay && !_isAttack)
        {
            _controller.Shoot8Directions();
            _isAttack = true;
        }
        
        if(_timer >= _stateDuration)
        {
            _controller.ChangeState(_controller.MoveState);
        }
    }
}