using UnityEngine;

/// <summary>
/// 삽
/// </summary>
public class Bullet_Shovel : PlayerBulletBase
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        // 적의 일반 총알이 부딪힐 경우 적 총알 풀로 되돌리기
        if(collision.gameObject.CompareTag("EnemyNormalBullet"))
        {
            PoolingObject poolOb = collision.gameObject.GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }
}
