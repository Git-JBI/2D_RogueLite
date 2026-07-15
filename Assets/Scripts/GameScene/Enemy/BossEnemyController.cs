using UnityEngine.Events;

public abstract class BossEnemyController : EnemyControllerBase
{
    public UnityAction OnBossDead;
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