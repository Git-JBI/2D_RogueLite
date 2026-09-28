using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 한 발씩 적을 정조준해서 쏘는 권총 무기
/// </summary>
public class WeaponPistol : WeaponControllerBase
{
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
        
        if(enemies.Count > 0)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if(distance > Vector2.Distance(transform.position, enemies[i].transform.position))
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

            GameObject bullet;

            bullet = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Bullets/Bullet_Gun", transform);

            Bullet_Pistol bulletPrefab = bullet.GetComponent<Bullet_Pistol>();

            bulletPrefab.Init(_heroModel, _damageMagnification, transform.position, _bulletDir, _bulletSpeed);

            yield return new WaitForSeconds(_coolTime);
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();

        if (_shootRoutine == null)
            _shootRoutine = StartCoroutine(ShootBullets());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectRadius);
    }

    protected override void CalculateOtherStats()
    {
        _bulletSpeed = _weaponData.GetStatValue(WeaponStatType.BulletSpeed, _level);
        _coolTime = _weaponData.GetStatValue(WeaponStatType.Cooltime, _level);
    }
}
