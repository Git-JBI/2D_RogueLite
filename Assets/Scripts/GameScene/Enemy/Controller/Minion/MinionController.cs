using UnityEngine;
using UnityEngine.Events;

public abstract class MinionController : EnemyControllerBase
{
    private int _curStage;
    private int _expType;

    protected IState _damagedState;

    public event UnityAction<MinionController> OnMinionDead;

    public IState DamagedState => _damagedState;

    protected override void Awake()
    {
        base.Awake();

        _deadState = new EnemyDeadState(this, 0.5f);
        _damagedState = new MinionDamagedState(this, 0.2f);
    }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    /// <param name="target">플레이어 캐릭터</param>
    /// <param name="data">적 설정 데이터</param>
    /// <param name="stage">스테이지</param>
    /// <param name="expType">EXP아이템을 결정해줄 타입</param>
    public void Init(Transform target, SO_EnemyData data, int stage, int expType)
    {
        // 타겟 설정
        _target = target;

        // 생성 위치 설정
        transform.position = RandomSpawnPosition();

        // 스테이지 값 설정
        // 최초 설정 시 (모델이 없을 경우)
        if (_model == null)
        {
            _curStage = stage;
            // 모델 생성
            _model = new EnemyModel(data, _curStage);
        }
        // 스테이지가 달라졌을 경우
        else if(_curStage != stage)
        {
            _curStage = stage;
            // 모델 값 초기화
            _model.Respawn(data, _curStage);
        }
        // 스테이지가 달라지지않고 리스폰될 경우
        else
        {
            // 모델 값 초기화
            _model.Respawn();
        }

        // 드랍할 경험치 타입
        _expType = expType;

        // 리지드바디 활성화
        SetRigidBodyActive(true);
        // 콜라이더 활성화
        SetColliderActive(true);
        // 상태 초기화
        SetStartState();
    }

    /// <summary>
    /// 경험치 아이템을 생성하는 함수
    /// </summary>
    protected void SpawnExpItem()
    {
        GameObject expItem;

        expItem = GameManager.Instance.PoolManager.GetFromPool($"Prefabs/Item/Item_Exp", transform);

        ExpItem item = expItem.GetComponent<ExpItem>();

        item.Init(transform.position, _expType);
    }

    /// <summary>
    /// 미니언의 죽음 시 실행될 함수
    /// </summary>
    public override void Remove()
    {
        // 죽음 이벤트 실행
        OnMinionDead?.Invoke(this);

        // 이벤트 비우기
        OnMinionDead = null;

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
}