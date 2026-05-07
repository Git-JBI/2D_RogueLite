using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TeleportEnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject _enemyBody;

    //TODO 추후 스포너에서 받아오기
    [SerializeField] private HeroController _target;
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

    private void Awake()
    {
        _model = GetComponent<EnemyModel>();
        _rigid = GetComponent<Rigidbody2D>();

        _spriteController = GetComponentInChildren<EnemySpriteController>();
        _collider = GetComponentInChildren<Collider2D>();

        _idleState = new TeleportEnemyIdleState(this);
        _teleportState = new TeleportEnemyTeleportState(this);
        _deadState = new TeleportEnemyDeadState(this);
        ChangeState(_idleState);
    }

    private void Update()
    {
        _curState.OnStateUpdate();
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
        _rigid.simulated = true;

        // 콜라이더 켜기
        _collider.enabled = true;
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
        _rigid.simulated = false;
        // 콜라이더 끄기
        _collider.enabled = false;
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
        throw new System.NotImplementedException();
    }
    #endregion
}