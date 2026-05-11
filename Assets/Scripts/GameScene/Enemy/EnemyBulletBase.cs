using UnityEngine;

public class EnemyBulletBase : MonoBehaviour
{
    protected EnemyModel _enemyModel;

    public virtual void Init(EnemyModel model)
    {
        _enemyModel = model;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable idamageable = collision.gameObject.GetComponent<IDamageable>();
            
            idamageable.TakeDamage(_enemyModel.Damage);
        }
    }
}
