using UnityEngine;

public class WO_Kunai : TrailWeaponObjectBase
{
    private float _pierceCount;
    private float _bulletSpeed;
    private Vector2 _startPos;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * _bulletSpeed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, _startPos) > 10)
            ReturnToPool();
    }

    public void Init(HeroModel model, float magnification, Vector2 startPos, Vector2 dir, float pierceCount, float bulletSpeed)
    {
        base.Init(model, magnification);
        transform.position = startPos;
        _startPos = startPos;
        transform.up = dir;

        _pierceCount = pierceCount;
        _bulletSpeed = bulletSpeed;

        transform.SetParent(null);

        StartTrail();
    }

    protected override void OnHit(Collider2D collision, float damage)
    {
        base.OnHit(collision, damage);

        if(_pierceCount >= 1)
        {
            _pierceCount--;
        }
        else
        {
            ReturnToPool();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
