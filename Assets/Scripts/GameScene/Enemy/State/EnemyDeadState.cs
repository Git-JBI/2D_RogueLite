using System.Collections;
using UnityEngine;

public class EnemyDeadState : IState
{
    private EnemyController _enemy;

    private float _timer;

    public EnemyDeadState(EnemyController enemy)
    {
        _enemy = enemy;
    }

    public void OnStateEnter()
    {
        // 타이머 초기화
        _timer = 0;

        // 리지드바디 비활성화
        _enemy.SetRigidbodyActive(false);

        // 콜라이더 비활성화
        _enemy.SetColliderActive(false);

        // 죽음 애니메이션 트리거실행
        _enemy.SpriteController.Dead();
    }

    public void OnStateExit()
    {
        _enemy.Dead();
    }

    public void OnStateUpdate()
    {
        if(_timer < 0.5f)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            OnStateExit();
        }
    }
}
