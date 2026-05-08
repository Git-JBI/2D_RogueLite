using UnityEngine;

public class EnemyBulletBase : MonoBehaviour
{
    protected EnemyModel _enemyModel;
    protected float _bulletSpeed;
    protected float _timer;

    public void Init(EnemyModel model, Vector2 startPos, Vector2 dir, float bulletSpeed)
    {
        _enemyModel = model;
        transform.position = startPos;
        transform.up = dir;
        _bulletSpeed = bulletSpeed;

        //TODO 추후 2가지로 나뉘도록 스크립트 변경(부모해제, 그대로)
        // transform.SetParent(null);
        _timer = 0;
    }

    protected void FixedUpdate()
    {
        transform.Translate(Vector2.up * _bulletSpeed * Time.deltaTime);
        _timer += Time.deltaTime;

        // 3초 지날 시 총알 제거
        if(_timer >= 3)
        {
            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player");
            // IDamageable idamageable = collision.gameObject.GetComponent<IDamageable>();
            // 
            // idamageable.TakeDamage(_enemyModel.Damage);
            // 
            // PoolingObject poolOb = GetComponent<PoolingObject>();
            // poolOb.ReturnToPool();
        }
    }
}
