using UnityEngine;

/// <summary>
/// 무한하게 돌아가며 적의 총알을 막아주는 각성 삽
/// </summary>
public class AwakeningShovel : AwakeningWeaponBase
{
    private PlayerBulletBase _shovel;

    private float _bulletCount;
    private float _rotSpeed;
    private float _radius;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, _rotSpeed);
    }

    public override void Init(HeroModel model)
    {
        base.Init(model);

        _weaponType = _weaponData.WeaponType;

        _shovel = Resources.Load<PlayerBulletBase>("Prefabs/Bullets/Bullet_Shovel");

        _radius = 4;

        _bulletCount = _weaponData.GetStatValue(WeaponStatType.BulletCount);
        _rotSpeed = _weaponData.GetStatValue(WeaponStatType.RotSpeed);
    }

    public override void EnableWeapon()
    {
        base.EnableWeapon();

        SpawnShovels();
    }

    private void SpawnShovels()
    {
        float angle = 360 / _bulletCount;

        for (int i = 0; i < _bulletCount; i++)
        {
            PlayerBulletBase shovel = Instantiate(_shovel);

            shovel.Init(_heroModel, _damageMagnification);

            Vector2 dir = new Vector2(Mathf.Cos(i * angle * Mathf.Deg2Rad), Mathf.Sin(i * angle * Mathf.Deg2Rad));

            shovel.transform.up = dir;

            shovel.transform.SetParent(transform, false);

            shovel.transform.localPosition = dir * _radius;
        }
    }
}
