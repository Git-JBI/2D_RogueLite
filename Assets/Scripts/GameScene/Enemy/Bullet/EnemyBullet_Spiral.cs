using UnityEngine;

public class EnemyBullet_Spiral : MonoBehaviour
{
    // 데미지
    private float _damage;
    // 회전 속도
    private float _rotSpeed;
    // 사용할 총알 갯수
    private int _bulletCount;
    // 사용할 총알의 속도
    private float _bulletSpeed;
    // 타이머
    private float _timer;

    private void FixedUpdate()
    {
        // 회전
        transform.Rotate(0, 0, _rotSpeed);

        // 시간 흐르기
        _timer += Time.deltaTime;

        //TODO 추후 유지 시간 받아와서 적용 
        if (_timer >= 3)
        {
            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }

    public void Init(Vector2 pos, float damage)
    {
        // 시작 위치 설정
        transform.position = pos;
        // 데미지 설정
        _damage = damage;
        // 총알 갯수 설정
        _bulletCount = 8;
        // 총알 속도 설정
        _bulletSpeed = 7;
        // 회전 속도 설정
        _rotSpeed = 1.5f;
        // 타이머 초기화
        _timer = 0;
        // 부모로부터 귀속 해제
        transform.SetParent(null);
        // 총알 발사
        ShootBullets(_bulletCount, _bulletSpeed);
    }

    /// <summary>
    /// 원하는 수량의 총알을 정해진 속도로 방향을 나눠서 발사
    /// </summary>
    /// <param name="bulletCount">총알 갯수</param>
    /// <param name="bulletSpeed">총알 속도</param>
    private void ShootBullets(int bulletCount, float bulletSpeed)
    {
        for(int i = 0; i < bulletCount; i++)
        {
            float angle = i * 360 / bulletCount;

            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            Vector2 dir = rotation * Vector2.up;

            GameObject bullet;
            bullet = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Bullets/Bullet_Enemy", transform);

            EnemyBulletBase bulletBase = bullet.GetComponent<EnemyBulletBase>();

            bulletBase.Init(transform.position, dir, _damage, _bulletSpeed, true);
        }
    }
}
