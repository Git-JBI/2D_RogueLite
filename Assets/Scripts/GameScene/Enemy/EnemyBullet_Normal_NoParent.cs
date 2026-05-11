using UnityEngine;

public class EnemyBullet_Normal_NoParent : EnemyBullet_Normal_HaveParent
{
    public override void Init(Vector2 pos, Vector2 dir, float bulletSpeed)
    {
        base.Init(pos, dir, bulletSpeed);

        // 부모 해제
        transform.SetParent(null);
    }
}
