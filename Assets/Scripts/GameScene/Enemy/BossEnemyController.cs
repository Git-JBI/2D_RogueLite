using UnityEngine;
using UnityEngine.Events;

public abstract class BossEnemyController : EnemyControllerBase
{
    public UnityAction OnBossDead;

    public void Init(Transform target, Vector2 spawnPos, SO_EnemyData data, int stage)
    {
        base.Init(target, spawnPos);

        // 모델 값 초기화
        _model = new EnemyModel(data, stage);

        // 상태 초기화
        SetStartState();
    }
    public override void Dead()
    {
        // 죽음 이벤트 실행
        OnBossDead?.Invoke();

        // 현재 상태 비우기
        base.Dead();

        // 제거
        Destroy(gameObject);
    }
}