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

    private void Awake()
    {
        //TODO 레벨 설정 방식 변경
        _level = 1;
        _radius = 3;
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, _rotSpeed);
    }

    //TODO 여기부터 작업시작
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
        _bulletCount = _weaponData.GetStatValue(WeaponStatType.BulletCount, _level - 1);
        _rotSpeed = _weaponData.GetStatValue(WeaponStatType.RotSpeed, _level - 1);
    }
}
