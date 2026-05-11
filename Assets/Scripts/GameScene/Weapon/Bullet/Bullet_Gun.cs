using UnityEngine;

public class Bullet_Gun : PlayerBulletBase
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

        //TODO 거리 말고 지정한 시간이 지날 경우 총알 제거
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
            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }
}
