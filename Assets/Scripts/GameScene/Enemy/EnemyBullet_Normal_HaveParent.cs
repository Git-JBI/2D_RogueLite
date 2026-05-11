using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemyBullet_Normal_HaveParent : EnemyBulletBase
{
    private float _bulletSpeed;
    private float _timer;

    public virtual void Init(Vector2 pos, Vector2 dir, float bulletSpeed)
    {
        transform.position = pos;
        transform.up = dir;
        _bulletSpeed = bulletSpeed;
        _timer = 0;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * _bulletSpeed * Time.deltaTime);

        _timer += Time.deltaTime;

        //TODO 추후 지정한 시간이 지날 경우 총알 제거로 변경
        if (_timer > 3)
        {
            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }
}
