using System.Collections;
using UnityEngine;

public class TeleportBossController : BossEnemyController
{
    private Coroutine _teleportCoroutine;

    private TeleportBossIdleState _idleState;
    private TeleportEnemyTeleportState _teleportState;

    public EnemySpriteController SpriteController => _spriteController;

    protected override void Awake()
    {
        base.Awake();

        _idleState = new TeleportBossIdleState(this);
        _teleportState = new TeleportEnemyTeleportState(this);
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
        SetRigidBodyActive(true);

        // 콜라이더 켜기
        SetColliderActive(true);
    }
    public void FinishIdle()
    {
        ChangeState(_teleportState);
    }
    protected override void SetStartState()
    {
        ChangeState(_idleState);
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
            bullet = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Bullets/Bullet_Enemy", transform);

            EnemyBulletBase spiralBullet = bullet.GetComponent<EnemyBulletBase>();

            spiralBullet.Init(transform.position, dir, _model.Damage, 3, false);
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

        bullet.Init(transform.position, _model.Damage);
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
        SetRigidBodyActive(false);
        // 콜라이더 끄기
        SetColliderActive(false);
        // 본체 위치 상승
        transform.localPosition = Vector3.up * 3;
        // 적 위치 변경
        transform.position = teleportPos;

        // 일정 시간동안 본체 하강
        while(timer < 1)
        {
            timer += Time.deltaTime;
            downSpeed += Time.deltaTime * 5;
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, Vector2.zero, downSpeed * Time.deltaTime);
            yield return null;
        }
        
        // 본체의 위치 0,0,0으로 조정
        transform.localPosition = Vector3.zero;

        // Idle상태 진입
        ChangeState(_idleState);
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }
    public override void HandleMoveCompleted()
    {
        throw new System.NotImplementedException();
    }
}