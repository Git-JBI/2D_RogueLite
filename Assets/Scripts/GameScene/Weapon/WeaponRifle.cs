using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 한 번 적을 정조준 해서 여러발 쏘는 라이플 무기
/// </summary>
public class WeaponRifle : WeaponBase
{
    private float _bulletCount;
    private float _bulletSpeed;
    private float _fireDelay;
    private float _coolTime;

    private float _detectRadius;
    private Vector2 _targetPos;
    private Vector2 _bulletDir;

    private LayerMask _enemyMask;

    private Coroutine _shootRoutine;

    protected override void Awake()
    {
        base.Awake();

        _enemyMask = LayerMask.GetMask("Enemy");

        _detectRadius = 5.0f;
    }

    private Vector2 SetBulletDir()
    {
        List<Collider2D> enemies = new List<Collider2D>();

        float distance = _detectRadius;

        enemies = Physics2D.OverlapCircleAll(transform.position, _detectRadius, _enemyMask).ToList<Collider2D>();

        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (distance > Vector2.Distance(transform.position, enemies[i].transform.position))
                {
                    distance = Vector2.Distance(transform.position, enemies[i].transform.position);
                    _targetPos = enemies[i].transform.position;
                }
            }
        }
        else
        {
            _targetPos = Random.insideUnitCircle;
        }

        return (_targetPos - (Vector2)transform.position).normalized;
    }

    private IEnumerator ShootBullets()
    {
        while(_level > 0)
        {
            _bulletDir = SetBulletDir();

            for(int i = 0; i < _bulletCount; i++)
            {
                GameObject bullet;

                //TODO 라이플쪽 총알 변경
                bullet = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Bullets/Bullet_Gun", transform);

                Bullet_Gun bulletPrefab = bullet.GetComponent<Bullet_Gun>();

                bulletPrefab.Init(_heroModel, _damageMagnification);
                bulletPrefab.Init(transform.position, _bulletDir, _bulletSpeed);

                // 총알 간격 시간
                yield return new WaitForSeconds(_fireDelay);
            }
            yield return new WaitForSeconds(_coolTime);
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();

        if (_shootRoutine == null)
            _shootRoutine = StartCoroutine(ShootBullets());
    }

    protected override void CalculateStats()
    {
        base.CalculateStats();
        
        _bulletCount = _weaponData.GetStatValue(WeaponStatType.BulletCount, _level);
        _bulletSpeed = _weaponData.GetStatValue(WeaponStatType.BulletSpeed, _level);
        _fireDelay = _weaponData.GetStatValue(WeaponStatType.FireDelay, _level);
        _coolTime = _weaponData.GetStatValue(WeaponStatType.Cooltime, _level);
    }
}
