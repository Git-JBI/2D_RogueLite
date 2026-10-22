using System.Collections;
using UnityEngine;

public class KunaiController : ShootWeaponBase
{
    private float _pierceCount;

    protected override IEnumerator ShootBullets()
    {
        while(true)
        {
            _bulletDir = SetBulletDir();

            for(int i = 0; i < _bulletCount; i++)
            {
                GameObject kunaiObject = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Bullets/Bullet_Kunai", transform);

                WO_Kunai kunai = kunaiObject.GetComponent<WO_Kunai>();

                kunai.Init(_heroModel, _damageMagnification, transform.position, _bulletDir, _pierceCount, _bulletSpeed);

                // Äí³ªÀÌ °£ ½Ã°£
                yield return new WaitForSeconds(_fireDelay);
            }
            // ÇÑ ½ÎÀÌÅ¬ µ· ÄðÅ¸ÀÓ ½Ã°£
            yield return new WaitForSeconds(_coolTime);
        }
    }

    protected override void CalculateOtherStats()
    {
        _bulletCount = _weaponData.GetStatValue(WeaponStatType.BulletCount, _level);
        _bulletSpeed = _weaponData.GetStatValue(WeaponStatType.BulletSpeed, _level);
        _fireDelay = _weaponData.GetStatValue(WeaponStatType.FireDelay, _level);
        _coolTime = _weaponData.GetStatValue(WeaponStatType.Cooltime, _level);
        _pierceCount = _weaponData.GetStatValue(WeaponStatType.PierceCount, _level);
    }
}
