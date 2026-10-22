using System.Collections;
using UnityEngine;

/// <summary>
/// 한 번 적을 정조준 해서 여러발 쏘는 라이플 무기
/// </summary>
public class WeaponRifle : ShootWeaponBase
{
    protected override IEnumerator ShootBullets()
    {
        while(_level > 0)
        {
            _bulletDir = SetBulletDir();

            for(int i = 0; i < _bulletCount; i++)
            {
                GameObject bullet;

                //TODO 라이플쪽 총알 변경
                bullet = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Bullets/Bullet_Gun", transform);

                Bullet_Pistol bulletPrefab = bullet.GetComponent<Bullet_Pistol>();

                bulletPrefab.Init(_heroModel, _damageMagnification, transform.position, _bulletDir, _bulletSpeed);

                // 총알 간격 시간
                yield return new WaitForSeconds(_fireDelay);
            }
            // 쿨타임 시간
            yield return new WaitForSeconds(_coolTime);
        }
    }

    protected override void CalculateOtherStats()
    {
        _bulletCount = _weaponData.GetStatValue(WeaponStatType.BulletCount, _level);
        _bulletSpeed = _weaponData.GetStatValue(WeaponStatType.BulletSpeed, _level);
        _fireDelay = _weaponData.GetStatValue(WeaponStatType.FireDelay, _level);
        _coolTime = _weaponData.GetStatValue(WeaponStatType.Cooltime, _level);
    }
}
