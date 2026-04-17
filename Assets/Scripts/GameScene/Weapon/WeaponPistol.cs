using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPistol : MonoBehaviour
{
    //TODO ResourceLoad·Î º¯°æ
    [SerializeField] private Bullet_Gun _bullet;

    private int _level;
    private int _bulletCount;
    private float _bulletSpeed;

    private Coroutine _shootRoutine;

    private List<Bullet_Gun> _bullets = new();

    private int Level => _level;

    private void Awake()
    {
        _level = 0;
        _bulletCount = 0;
        _bulletSpeed = 1f;
    }

    private IEnumerator ShootBullets()
    {
        while(true)
        {

        }
    }

    public void LevelUp()
    {
        if (_level == 5)
            return;

        if(_shootRoutine == null)
        {
            _shootRoutine = StartCoroutine(ShootBullets());
        }

        _level++;
        _bulletCount++;
        _bulletSpeed += 0.5f;
    }
}
