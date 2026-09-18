using UnityEngine;

/// <summary>
/// 삽
/// </summary>
public class WO_Shovel : PlayerWeaponObjectBase
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        // 적의 일반 총알이 부딪힐 경우 적 총알 풀로 되돌리기
        if(collision.CompareTag("EnemyNormalBullet"))
        {
            PoolingObject poolOb = collision.gameObject.GetComponent<PoolingObject>();

            if (poolOb == null)
                return;

            poolOb.ReturnToPool();
        }
    }
}
