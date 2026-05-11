using UnityEngine;

public class TeleportEnemyIdleState : IState
{
    private TeleportEnemyController _controller;

    private float _timer;
    private bool _isAttackTime;

    public TeleportEnemyIdleState(TeleportEnemyController controller)
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
            Debug.Log("2.5초 지남");
            _controller.Shoot8Directions();
            _isAttackTime = true;
        }
        
        if(_timer >= 5.0f)
        {
            Debug.Log("5초 지남");
            _controller.ChangeState(_controller.TeleportState);
        }
    }
}
