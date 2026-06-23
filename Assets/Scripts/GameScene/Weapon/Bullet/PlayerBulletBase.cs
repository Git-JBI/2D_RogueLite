using UnityEngine;

public class PlayerBulletBase : MonoBehaviour
{
    protected HeroModel _model;
    protected float _damageMagnification;

    public void Init(HeroModel model, float magnification)
    {
        _model = model;
        _damageMagnification = magnification;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable idamageable = collision.gameObject.GetComponentInParent<IDamageable>();

            idamageable.TakeDamage(_model.Damage * _damageMagnification);
        }
        else if(collision.gameObject.CompareTag("Boss"))
        {
            IDamageable idamageable = collision.gameObject.GetComponentInParent<IDamageable>();

            idamageable.TakeDamage(_model.Damage * _damageMagnification);
        }
    }
}
