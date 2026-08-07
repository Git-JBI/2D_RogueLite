using UnityEngine;

public class EnemyDeadState : IState
{
    private EnemyControllerBase _controller;

    private float _timer;

    public EnemyDeadState(EnemyControllerBase enemy)
    {
        _controller = enemy;

        // 타이머 초기화
        _timer = 0;
    }

    public void OnStateEnter()
    {
        // 타이머 초기화
        _timer = 0;

        // 리지드바디 비활성화
        _controller.SetRigidBodyActive(false);

        // 콜라이더 비활성화
        _controller.SetColliderActive(false);

        // 죽음트리거 실행
        _controller.DeadTrigger();
    }

    public void OnStateExit()
    {
    }
        

    public void OnStateUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= 0.5f)
        {
            // 죽음 상태 끝날 시 실행할 함수
            _controller.HandleDeadCompleted();
        }
    }
}