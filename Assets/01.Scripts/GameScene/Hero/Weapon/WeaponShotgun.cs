using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponShotgun : WeaponControllerBase
{
    private float _spreadAngle;
    private float _bulletCount;
    private float _bulletSpeed;
    private float _coolTime;

    private float _detectRadius;
    private Vector2 _targetPos;
    private Vector2 _bulletDir;

    private LayerMask _enemyMask;

    private Coroutine _shootRoutine;

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
        while (_level > 0)
        {
            _bulletDir = SetBulletDir();

            for (int i = 0; i < _bulletCount; i++)
            {
                Vector2 randomBulletDir = Quaternion.Euler(0, 0, Random.Range(-_spreadAngle, _spreadAngle)) * _bulletDir;

                GameObject bullet;

                //TODO 샷건쪽 총알 변경
                bullet = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Bullets/Bullet_Gun", transform);

                Bullet_Pistol bulletPrefab = bullet.GetComponent<Bullet_Pistol>();

                bulletPrefab.Init(_heroModel, _damageMagnification, transform.position, randomBulletDir, _bulletSpeed);
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

        _spreadAngle = _weaponData.GetStatValue(WeaponStatType.SpreadAngle, _level);
        _bulletCount = _weaponData.GetStatValue(WeaponStatType.BulletCount, _level);
        _bulletSpeed = _weaponData.GetStatValue(WeaponStatType.BulletSpeed, _level);
        _coolTime = _weaponData.GetStatValue(WeaponStatType.Cooltime, _level);
    }

    protected override void CalculateOtherStats()
    {
        throw new System.NotImplementedException();
    }
}
