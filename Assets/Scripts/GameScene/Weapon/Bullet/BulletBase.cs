using UnityEngine;

public class BulletBase : MonoBehaviour
{
    private float _damage;

    private void Awake()
    {
        _damage = 5.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable idamageable = collision.gameObject.GetComponentInParent<IDamageable>();

            idamageable.TakeDamage(_damage);
        }
    }
}
