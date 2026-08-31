using UnityEngine;

public class StandingMinionNormalState : IState
{
    private StandingMinionController _controller;

    private bool _checkShoot;
    private float _timer;
    private float _shootTime;
    private float _updateTime;

    public StandingMinionNormalState(StandingMinionController controller, float shootTime, float updateTime)
    {
        _controller = controller;

        // 발사 시간
        _shootTime = shootTime;

        // Update를 진행할 시간
        _updateTime = updateTime;
    }

    public void OnStateEnter()
    {
        // 타이머 초기화
        _timer = 0;

        // 발사 여부 초기화
        _checkShoot = false;
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {
        _timer += Time.deltaTime;

        if(_timer >= _shootTime && !_checkShoot)
        {
            _controller.ShootBullet();
            _checkShoot = true;
        }
        if(_timer >= _updateTime)
        {
            _controller.CheckDistance();
            _timer = 0.0f;
            _checkShoot = false;
        }
    }
}