using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TeleportEnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject _enemyBody;

    //TODO 추후 스포너에서 받아오기
    private Transform _target;
    private EnemyModel _model;
    private EnemySpriteController _spriteController;
    private Rigidbody2D _rigid;
    private Collider2D _collider;

    private Coroutine _teleportCoroutine;

    private IState _curState;

    public event UnityAction OnDead;

    private TeleportEnemyIdleState _idleState;
    private TeleportEnemyTeleportState _teleportState;
    private TeleportEnemyDeadState _deadState;

    public TeleportEnemyIdleState IdleState => _idleState;
    public TeleportEnemyTeleportState TeleportState => _teleportState;

    public EnemySpriteController SpriteController => _spriteController;

    private void Awake()
    {
        _model = GetComponent<EnemyModel>();
        _rigid = GetComponent<Rigidbody2D>();

        _spriteController = GetComponentInChildren<EnemySpriteController>();
        _collider = GetComponentInChildren<Collider2D>();

        _idleState = new TeleportEnemyIdleState(this);
        _teleportState = new TeleportEnemyTeleportState(this);
        _deadState = new TeleportEnemyDeadState(this);

        //TODO 추후 spawner에서
        _model.Init();

        ChangeState(_idleState);
    }

    private void Update()
    {
        _curState.OnStateUpdate();
    }

    public void Init(Transform target)
    {
        _target = target;
    }

    /// <summary>
    /// 상태변화 함수
    /// </summary>
    /// <param name="state">설정할 상태</param>
    public void ChangeState(IState state)
    {
        if (_curState == _deadState)
            return;

        if (_curState != null)
            _curState.OnStateExit();

        _curState = state;

        _curState.OnStateEnter();
    }

    /// <summary>
    /// 리지드바디 활성화 설정
    /// </summary>
    /// <param name="x"></param>
    public void SetRigidbodyActive(bool x)
    {
        _rigid.simulated = x;
    }

    /// <summary>
    /// 콜라이더 활성화 설정
    /// </summary>
    /// <param name="x"></param>
    public void SetColliderActive(bool x)
    {
        _collider.enabled = x;
    }

    /// <summary>
    /// 텔레포트 시작
    /// </summary>
    public void StartTeleport()
    {
        _teleportCoroutine = StartCoroutine(TeleportRoutine());
    }

    /// <summary>
    /// 텔레포트 끝
    /// </summary>
    public void EndTeleport()
    {
        // 텔레포트 코루틴 끝내기
        StopCoroutine(_teleportCoroutine);

        // 리지드바디 켜기
        SetRigidbodyActive(true);

        // 콜라이더 켜기
        SetColliderActive(true);
    }

    public void Dead()
    {
        // 죽음 이벤트 실행
        OnDead?.Invoke();

        // 현재 상태 비우기
        _curState = null;

        // 이벤트 비우기
        OnDead = null;

        // 제거
        PoolingObject poolOb = GetComponent<PoolingObject>();

        if (poolOb)
            poolOb.ReturnToPool();
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// 8방향 총쏘기
    /// </summary>
    public void Shoot8Directions()
    {
        int bulletCount = 8;

        for(int i = 0; i < bulletCount; i++)
        {
            float angle = i * 360 / bulletCount;

            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            Vector2 dir = rotation * Vector2.up;

            GameObject bullet;
            bullet = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Bullets/Bullet_Enemy_NoParent", transform);

            EnemyBullet_Normal_NoParent spiralBullet = bullet.GetComponent<EnemyBullet_Normal_NoParent>();

            spiralBullet.Init(_model);
            spiralBullet.Init(transform.position, dir, 7);
        }
    }

    /// <summary>
    /// 8방향 회전하는 총쏘기
    /// </summary>
    public void ShootSpiral8Directions()
    {
        GameObject spiralBullet;
        spiralBullet = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Bullets/Bullet_Enemy_Spiral8Dir", transform);

        EnemyBullet_Spiral bullet = spiralBullet.GetComponent<EnemyBullet_Spiral>();

        bullet.Init(_model);
        bullet.Init(transform.position);
    }

    /// <summary>
    /// 텔레포트 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator TeleportRoutine()
    {
        float downSpeed = 1;
        float timer = 0;
        // 텔레포트할 랜덤한 위치(플레이어 캐릭터 기준)
        Vector2 teleportPos = Random.insideUnitCircle.normalized * 4.0f + (Vector2)_target.transform.position;
        // 리지드바디 끄기
        SetRigidbodyActive(false);
        // 콜라이더 끄기
        SetColliderActive(false);
        // 본체 위치 상승
        _enemyBody.transform.localPosition = Vector3.up * 3;
        // 적 위치 변경
        transform.position = teleportPos;

        // 일정 시간동안 본체 하강
        while(timer < 1)
        {
            timer += Time.deltaTime;
            downSpeed += Time.deltaTime * 5;
            _enemyBody.transform.localPosition = Vector2.MoveTowards(_enemyBody.transform.localPosition, Vector2.zero, downSpeed * Time.deltaTime);
            yield return null;
        }
        
        // 본체의 위치 0,0,0으로 조정
        _enemyBody.transform.localPosition = Vector3.zero;

        // Idle상태 진입
        ChangeState(_idleState);
    }

    #region Interface
    public void TakeDamage(float damage)
    {
        //TODO 플레이어한테 데미지 입음
        _model.TakeDamage(damage);

        if(_model.CurHp <= 0)
        {
            ChangeState(_deadState);
        }
        else
        {
            _spriteController.Damaged();
        }
    }
    #endregion
}