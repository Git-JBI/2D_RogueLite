using UnityEngine;

public class TeleportMinionController : MinionEnemyController
{
    private MinionTeleportState _teleportState;

    protected override void Awake()
    {
        base.Awake();

        _teleportState = new MinionTeleportState(this);
    }

    public void ShootBullet()
    {
        GameObject bullet;

        bullet = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Bullets/Bullet_Enemy", transform);

        EnemyBulletBase bulletBase = bullet.GetComponent<EnemyBulletBase>();

        bulletBase.Init(transform.position, DirToTarget(), _model.Damage, 3, false);
    }

    public override void Move()
    {
        // 일정거리 이상으로 플레이어와 멀어졌을 경우 다시 이동하는 방식
        float distance = Vector2.Distance(transform.position, _target.position);

        if(distance >= 20.0f)
        {
            transform.position = RandomSpawnPosition();
        }
    }
    public override void HandleMoveCompleted()
    {
        ChangeState(_teleportState);
    }
    public override void HandleDamagedCompleted()
    {
        ChangeState(_teleportState);
    }

    protected override void SetStartState()
    {
        _curState = _teleportState;
    }
}
