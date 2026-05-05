using UnityEngine;

public class Bullet_Shovel : BulletBase
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable idamageable = collision.gameObject.GetComponentInParent<IDamageable>();

            idamageable.TakeDamage(_model.Damage * _damageMagnification);
        }
    }
}
