using UnityEngine;

public class PlayerWeaponObjectBase : MonoBehaviour
{
    private const float PercentToMultiplier = 0.01f;

    private float _minionDamage;
    private float _bossDamage;

    protected float _damageMagnification;

    public void Init(HeroModel model, float magnification)
    {
        _minionDamage =
            (model.Damage + model.MinionDamage) * (1f + model.DamageIncreasePercent * PercentToMultiplier) * (1f + model.MinionDamageIncreasePercent * PercentToMultiplier);
        _bossDamage =
            (model.Damage + model.BossDamage) * (1f + model.DamageIncreasePercent * PercentToMultiplier) * (1f + model.BossDamageIncreasePercent * PercentToMultiplier);

        _damageMagnification = magnification;
    }

    protected virtual void OnHit(Collider2D collision, float damage)
    {
        IDamageable idamageable = collision.gameObject.GetComponentInParent<IDamageable>();

        if (idamageable == null)
            return;

        damage *= _damageMagnification;

        idamageable.TakeDamage(damage);
    }

    protected virtual void ReturnToPool()
    {
        PoolingObject poolOb = GetComponent<PoolingObject>();
        poolOb.ReturnToPool();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // 일반 적 탐지 시
        if (collision.CompareTag("Minion"))
        {
            OnHit(collision, _minionDamage);
        }
        // 보스 탐지 시
        else if(collision.CompareTag("Boss"))
        {
            OnHit(collision, _bossDamage);
        }
    }
}
