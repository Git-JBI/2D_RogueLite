using UnityEngine;

public class Bullet_Gun : BulletBase
{
    private float _moveSpeed;
    private Vector2 _startPos;

    public void Init(Vector2 pos, Vector2 dir, float movespeed)
    {
        transform.position = pos;
        _startPos = pos;
        transform.up = dir;
        _moveSpeed = movespeed;
        transform.SetParent(null);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * _moveSpeed * Time.fixedDeltaTime);

        // 거리 멀어지면 총알 제거
        if(Vector2.Distance(transform.position, _startPos) > 10)
        {
            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable idamageable = collision.gameObject.GetComponentInParent<IDamageable>();

            idamageable.TakeDamage(_model.Damage * _damageMagnification);

            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }
}
