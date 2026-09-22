using UnityEngine;

public class EnemyBulletBase : MonoBehaviour
{
    [Header("----- 총알 지속 시간 -----")]
    [SerializeField] private float _bulletLifeTime = 5f;

    private float _damage;
    private float _speed;

    public void Init(Vector2 pos, Vector2 dir, float damage, float speed, bool haveParent)
    {
        // 시작 위치 설정
        transform.position = pos;

        // 방향 설정
        transform.up = dir;

        // 데미지 설정
        _damage = damage;

        // 이동 속도 설정
        _speed = speed;

        // 시간 초기화
        _bulletLifeTime = 0;

        // 부모해제 여부 설정
        if (!haveParent)
            transform.SetParent(null);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);

        _bulletLifeTime += Time.deltaTime;

        if(_bulletLifeTime > 5)
        {
            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable idamageable = collision.gameObject.GetComponentInParent<IDamageable>();

            idamageable.TakeDamage(_damage, false);

            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }
}