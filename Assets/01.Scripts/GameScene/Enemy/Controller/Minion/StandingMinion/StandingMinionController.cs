using UnityEngine;

public class StandingMinionController : MinionController
{
    private Vector3 _currentPosition;

    protected override void Awake()
    {
        base.Awake();

        _normalState = new StandingMinionNormalState(this, 1.5f, 3.0f);

        _currentPosition = transform.position;
    }

    public void ShootBullet()
    {
        GameObject bullet;

        bullet = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Bullets/Bullet_Enemy", transform);

        EnemyBulletBase bulletBase = bullet.GetComponent<EnemyBulletBase>();

        bulletBase.Init(transform.position, DirToTarget(), _model.Damage, 3, false);
    }

    public void CheckDistance()
    {
        // 일정거리 이상으로 플레이어와 멀어졌을 경우 다시 이동하는 방식
        float distance = Vector2.Distance(transform.position, _target.position);

        if(distance >= 20.0f)
        {
            transform.position = RandomSpawnPosition();
            _currentPosition = transform.position;
        }
    }
}