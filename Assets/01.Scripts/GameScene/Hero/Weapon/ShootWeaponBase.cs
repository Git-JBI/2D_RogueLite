using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ShootWeaponBase : WeaponControllerBase
{
    private string[] _enemyMasks = { "Minion", "Boss" };

    protected float _bulletCount;
    protected float _bulletSpeed;
    protected float _fireDelay;
    protected float _coolTime;

    protected float _detectRadius;
    protected Vector2 _targetPos;
    protected Vector2 _bulletDir;

    protected LayerMask _enemyMask;

    protected Coroutine _shootRoutine;

    public override void Init(HeroModel model)
    {
        base.Init(model);

        _enemyMask = LayerMask.GetMask(_enemyMasks);

        _detectRadius = 5.0f;
    }

    protected Vector2 SetBulletDir()
    {
        List<Collider2D> enemies = new List<Collider2D>();

        float distance = _detectRadius;

        enemies = Physics2D.OverlapCircleAll(transform.position, _detectRadius, _enemyMask).ToList<Collider2D>();

        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (distance > Vector2.Distance(transform.position, enemies[i].transform.position))
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

    protected abstract IEnumerator ShootBullets();

    public override void Upgrade()
    {
        base.Upgrade();

        if (_shootRoutine == null)
            _shootRoutine = StartCoroutine(ShootBullets());
    }
}
