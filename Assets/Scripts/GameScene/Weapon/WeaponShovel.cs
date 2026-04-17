using System.Collections.Generic;
using UnityEngine;

public class WeaponShovel : MonoBehaviour
{
    //TODO ResourceLoad·Î º¯°æ
    [SerializeField]private BulletBase _shovel;

    private int _level;
    private int _bulletCount;
    private float _rotSpeed;
    private float _radius;

    private List<BulletBase> _shovels = new();

    public int Level => _level;

    private void Awake()
    {
        _level = 0;
        _bulletCount = 0;
        _rotSpeed = 1;
        _radius = 3;
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, _rotSpeed);
    }

    public void LevelUp()
    {
        if (_level == 5)
            return;
        _level++;
        _bulletCount++;
        _rotSpeed += 0.5f;

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
}
