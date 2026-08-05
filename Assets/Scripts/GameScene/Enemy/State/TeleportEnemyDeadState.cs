using UnityEngine;

public class TeleportEnemyDeadState : IState
{
    private EnemyControllerBase _controller;

    private float _timer;

    public TeleportEnemyDeadState(EnemyControllerBase controller)
    {
        _controller = controller;
    }
    
    //TODO 리지드바디, 콜라이더의 비활성화를 TeleportEnemy에서 하도록 설정
    public void OnStateEnter()
    {
        // 타이머 초기화
        _timer = 0;

        _controller.EnterDeadState();
        // 리지드바디 비활성화
        _controller.SetRigidBodyActive(false);
        
        // 콜라이더 비활성화
        _controller.SetColliderActive(false);
        
        //TODO 변경 필요
        // 죽음 애니메이션 실행
        // _controller.SpriteController.Dead();
    }

    public void OnStateExit()
    {
        _controller.ExitDeadState();
    }

    public void OnStateUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer > 0.5)
            OnStateExit();
    }
}