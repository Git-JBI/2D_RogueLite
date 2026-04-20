using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class WeaponPistol : MonoBehaviour
{
    //TODO ResourceLoad로 변경
    [SerializeField] private Bullet_Gun _bullet;

    private int _level;
    private int _bulletCount;
    private float _bulletSpeed;
    private float _detectRadius;

    private ContactFilter2D filter2D = new ContactFilter2D();
    private LayerMask _enemyMask;

    private Coroutine _shootRoutine;

    private int Level => _level;

    private void Awake()
    {
        _enemyMask = LayerMask.NameToLayer("Enemy");
        filter2D.SetLayerMask(_enemyMask);

        _level = 0;
        _bulletCount = 0;
        _bulletSpeed = 1f;
        _detectRadius = 50.0f;
    }

    //TODO 적 감지 구현 중
    private void SelectTarget()
    {
        List<Collider2D> enemies = new List<Collider2D>();
        Debug.Log(enemies.Count);
        Debug.Log(filter2D.layerMask.Equals(_enemyMask));

        Physics2D.OverlapCircle(transform.position, _detectRadius, filter2D, enemies);
        if(enemies.Count > 0)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                Debug.Log(enemies[i].name);
            }
        }
        Debug.Log(enemies.Count);
    }

    private IEnumerator ShootBullets()
    {
        while (_level > 0)
        {
            for (int i = 0; i < _bulletCount; i++)
            {
                Bullet_Gun bullet = Instantiate(_bullet);
                // bullet.Init(transform.position, SelectTarget(), _bulletSpeed);
                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(2.0f);
        }
    }

    public void LevelUp()
    {
        SelectTarget();

        if (_level == 5)
            return;

        if (_shootRoutine == null)
            //_shootRoutine = StartCoroutine(ShootBullets());

        _level++;
        _bulletCount++;
        _bulletSpeed += 0.5f;
    }
}
