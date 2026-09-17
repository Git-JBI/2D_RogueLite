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

    private void ReturnToPool()
    {
        PoolingObject poolOb = GetComponent<PoolingObject>();
        poolOb.ReturnToPool();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO 추후 일반 적과 보스의 데미지 주는 연산이 바뀔 예정이라 나눔(일몹뎀, 보뎀)
        // 일반 적 탐지 시
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable idamageable = collision.gameObject.GetComponentInParent<IDamageable>();

            idamageable.TakeDamage(_model.Damage * _damageMagnification);

            ReturnToPool();
        }
        // 보스 탐지 시
        else if(collision.gameObject.CompareTag("Boss"))
        {
            IDamageable idamageable = collision.gameObject.GetComponentInParent<IDamageable>();

            idamageable.TakeDamage(_model.Damage * _damageMagnification);

            ReturnToPool();
        }
    }
}
