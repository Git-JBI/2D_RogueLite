using System.Collections.Generic;
using UnityEngine;

public class WeaponShovel : WeaponBase
{
    //TODO ResourceLoad로 변경
    [SerializeField]private BulletBase _shovel;

    private float _bulletCount;
    private float _rotSpeed;
    private float _radius;

    private List<BulletBase> _shovels = new();

    protected override void Awake()
    {
        base.Awake();

        _radius = 3;
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, _rotSpeed);
    }

    public override void Upgrade()
    {
        base.Upgrade();

        RemoveBullets();

        SpawnBullets();
    }

    private void SpawnBullets()
    {
        float angle = 360 / _bulletCount;

        for(int i = 0; i < _bulletCount; i++)
        {
            BulletBase shovel = Instantiate(_shovel);

            Vector2 dir = new Vector2(Mathf.Cos(i * angle * Mathf.Deg2Rad), Mathf.Sin(i * angle * Mathf.Deg2Rad));

            shovel.transform.up = dir;

            shovel.transform.SetParent(transform, false);

            shovel.transform.localPosition = dir * _radius;

            _shovels.Add(shovel);
        }
    }

    private void RemoveBullets()
    {
        foreach(BulletBase bullet in _shovels)
        {
            Destroy(bullet.gameObject);
        }
        _shovels.Clear();
    }

    protected override void CalculateStats()
    {
        //TODO WeaponStatData의 GetValue에서 Max레벨 이상 락걸어둬서 괜찮은데 여기서도 제한 걸어둬야 함
        _damage = _weaponData.GetStatValue(WeaponStatType.Damage, _level - 1);
        _bulletCount = _weaponData.GetStatValue(WeaponStatType.BulletCount, _level - 1);
        _rotSpeed = _weaponData.GetStatValue(WeaponStatType.RotSpeed, _level - 1);
    }
}
