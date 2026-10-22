using System.Collections;
using UnityEngine;

/// <summary>
/// 적을 튕겨가며 공격하는 총알을 발사하는 각성 피스톨
/// </summary>
public class AwakeningPistol : AwakeningWeaponBase
{
    private float _coolTime;
    private float _bulletSpeed;
    private float _bounceCount;

    public override void Init(InGameHeroModel model)
    {
        base.Init(model);

        _weaponType = _weaponData.WeaponType;

        _coolTime = _weaponData.GetStatValue(WeaponStatType.Cooltime);
        _bulletSpeed = _weaponData.GetStatValue(WeaponStatType.BulletSpeed);
        _bounceCount = _weaponData.GetStatValue(WeaponStatType.BounceCount);
    }

    public override void EnableWeapon()
    {
        base.EnableWeapon();

        StartCoroutine(ShootRoutine());
    }
    
    /// <summary>
    /// 각성 무기가 활성화되면 시작할 총알 발사 루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            // 사용할 불릿 오브젝트
            GameObject bullet;

            // 불릿 불러오기, 스폰
            bullet = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Bullets/Bullet_Chase", transform);

            // 추적하는 불릿 컴포넌트
            WO_ChasingBullet bulletPrefab = bullet.GetComponent<WO_ChasingBullet>();

            // 불릿 초기화
            bulletPrefab.Init(_heroModel, _damageMagnification, _bulletSpeed, (int)_bounceCount, transform.position);

            // 쿨타임동안 대기
            yield return new WaitForSeconds(_coolTime);
        }
    }
}