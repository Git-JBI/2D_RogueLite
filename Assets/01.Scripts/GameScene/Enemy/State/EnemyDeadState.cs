using UnityEngine;

public class EnemyDeadState : IState
{
    private EnemyControllerBase _controller;

    private float _timer;
    private float _updateTime;

    public EnemyDeadState(EnemyControllerBase enemy, float time)
    {
        _controller = enemy;

        // 업데이트 진행 시간
        _updateTime = time;
    }

    public void OnStateEnter()
    {
        // 타이머 초기화
        _timer = 0;

        // 리지드바디 비활성화
        _controller.SetRigidBodyActive(false);

        // 콜라이더 비활성화
        _controller.SetColliderActive(false);

        // 죽음 애니메이션트리거 실행
        _controller.DeadAnimTrigger();
    }

    public void OnStateExit()
    {
    }
        

    public void OnStateUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= _updateTime)
        {
            // 죽음 상태 끝날 시 실행할 함수
            _controller.Remove();
        }
    }
}