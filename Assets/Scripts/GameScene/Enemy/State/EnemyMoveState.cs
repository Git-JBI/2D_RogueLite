using UnityEngine;

public class EnemyMoveState : IState
{
    private EnemyControllerBase _controller;

    private float _timer;

    public EnemyMoveState(EnemyControllerBase enemy)
    {
        _controller = enemy;

        // 타이머 초기화
        _timer = 0;
    }

    public void OnStateEnter()
    {
        // 타이머 초기화
        _timer = 0;
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {
        _timer += Time.deltaTime;
        _controller.Move();

        // 이동 상태 2.5초간 지속될 경우
        if (_timer > 2.5f)
            _controller.HandleMoveCompleted();
    }
}