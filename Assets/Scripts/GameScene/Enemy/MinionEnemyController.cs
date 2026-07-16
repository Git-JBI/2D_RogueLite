using UnityEngine;
using UnityEngine.Events;

public abstract class MinionEnemyController : EnemyControllerBase
{
    private MinionDamagedState _damagedState;

    public event UnityAction<MinionEnemyController> OnMinionEnemyDead;

    protected override void Awake()
    {
        base.Awake();

        _damagedState = new MinionDamagedState(this);
    }

    public void Init(Transform target, Vector2 spawnPos, SO_EnemyData data, int stage)
    {
        base.Init(target, spawnPos);

        // 리지드바디 활성화
        SetRigidBodyActive(true);

        // 콜라이더 활성화
        SetColliderActive(true);

        // 모델 값 초기화
        _model = new EnemyModel(data, stage);

        // 상태 초기화
        SetStartState();
    }

    public override void Dead()
    {
        // 죽음 이벤트 실행
        OnMinionEnemyDead?.Invoke(this);

        // 현재 상태 비우기
        base.Dead();

        // 이벤트 비우기
        OnMinionEnemyDead = null;

        // 아이템 스폰
        SpawnExpItem();

        // 풀로 되돌리기
        PoolingObject poolOb = GetComponent<PoolingObject>();
        poolOb.ReturnToPool();
    }

    protected override void Damaged()
    {
        base.Damaged();

        ChangeState(_damagedState);
    }

    
    public abstract void FinishDamageState();

    //TODO 추후 expitem의 종류 다양 (3종의 아이템 + 스테이지 진행 시간에 따른 확률)
    /// <summary>
    /// 아이템을 생성하는 함수
    /// </summary>
    protected void SpawnExpItem()
    {
        GameObject expItem;

        expItem = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Item/Item_Exp", transform);

        ExpItem item = expItem.GetComponent<ExpItem>();

        item.Init(transform.position);
    }
}