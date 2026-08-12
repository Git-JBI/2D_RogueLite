using UnityEngine;
using UnityEngine.Events;

public abstract class BossController : EnemyControllerBase
{
    public UnityAction OnBossDead;

    public void Init(Transform target, SO_EnemyData data, int stage)
    {
        // 타겟 설정(플레이어)
        _target = target;

        // 위치 설정
        transform.position = RandomSpawnPosition();

        // 모델 값 초기화
        _model = new EnemyModel(data, stage);

        // 상태 초기화
        SetStartState();
    }

    public override void Remove()
    {
        // 죽음 이벤트 실행
        OnBossDead?.Invoke();

        // 제거
        Destroy(gameObject);
    }
}