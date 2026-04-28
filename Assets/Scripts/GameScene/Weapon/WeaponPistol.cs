using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponPistol : MonoBehaviour
{
    private int _level;
    private int _bulletCount;
    private float _bulletSpeed;
    private float _detectRadius;
    private Vector2 _targetPos;
    private Vector2 _bulletDir;

    private LayerMask _enemyMask;

    private Coroutine _shootRoutine;

    private int Level => _level;

    private void Awake()
    {
        _enemyMask = LayerMask.GetMask("Enemy");

        _level = 0;
        _bulletCount = 0;
        _bulletSpeed = 1f;
        _detectRadius = 5.0f;
    }

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

            for (int i = 0; i < _bulletCount; i++)
            {
                GameObject bullet;

                bullet = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Bullets/Bullet_Gun", transform);

                Bullet_Gun bulletPrefab = bullet.GetComponent<Bullet_Gun>();

                bulletPrefab.Init(transform.position, _bulletDir, _bulletSpeed);
                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(2.0f);
        }
    }

    public void LevelUp()
    {
        if (_level == 5)
            return;

        _level++;
        _bulletCount++;
        _bulletSpeed += 0.5f;

        if (_shootRoutine == null)
            _shootRoutine = StartCoroutine(ShootBullets());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectRadius);
    }
}
