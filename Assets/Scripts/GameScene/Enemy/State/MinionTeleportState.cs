using UnityEngine;

public class MinionTeleportState : IState
{
    private TeleportMinionController _controller;

    private float _timer;
    private bool _checkShoot;

    public MinionTeleportState(TeleportMinionController controller)
    {
        _controller = controller;

        // 타이머 초기화
        _timer = 0;

        // 발사 여부 초기화
        _checkShoot = false;
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

        // 일정 시간 지나면 Move실행
        if(_timer > 1.5f && !_checkShoot)
        {
            _checkShoot = true;
            _controller.ShootBullet();
        }
        else if(_timer > 3.0f)
        {
            _controller.Move();
            _controller.HandleMoveCompleted();
        }
    }
}