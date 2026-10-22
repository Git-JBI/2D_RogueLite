using System;
using UnityEngine;

public class WO_ChasingBullet : PlayerWeaponObjectBase
{
    private float _bulletSpeed;
    private int _maxBounceCount;
    private int _curBounceCount;
    private Vector2 _startPos;

    private LayerMask _enemyMask;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * _bulletSpeed * Time.fixedDeltaTime);

        // 감지 거리 밖으로 벗어났을 경우 풀로 되돌리기
        if(Vector2.Distance(transform.position, _startPos) > 20)
        {
            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }

    public void Init(InGameHeroModel model, float damage, float bulletSpeed, int bounceCount, Vector2 pos)
    {
        _damageMagnification = damage;
        _bulletSpeed = bulletSpeed;
        _maxBounceCount = bounceCount;
        _curBounceCount = _maxBounceCount;
        _startPos = pos;

        // 부모 설정 해제
        transform.SetParent(null);

        // 레이어마스크 값이 없을 시 초기화
        if (_enemyMask.value == 0)
            _enemyMask = LayerMask.GetMask("Enemy");

        // 시작 방향 설정
        transform.position = pos;

        // 가장 가까운 적 방향
        Vector2 dir = GetDirToEnemy();

        // 가장 가까운 적이 없을 경우(Vector.zero일 경우)
        if(dir == Vector2.zero)
        {
            Vector2 randomPos = UnityEngine.Random.insideUnitCircle;
            dir = (randomPos - (Vector2)transform.position).normalized;
        }

        // 위쪽 방향 설정
        transform.up = dir;
    }

    /// <summary>
    /// 적을 향하는 Vector2를 반환하는 함수
    /// </summary>
    /// <returns>가장 가까운 or 그 다음으로 가까운 적을 향하는 벡터</returns>
    private Vector2 GetDirToEnemy()
    {
        // 적 배열
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 20, _enemyMask);

        // 발견된 적이 없을 경우
        if (enemies.Length == 0)
            return Vector2.zero;

        // 가장 가까운 적을 제외할지 안할지
        int skipCount = (_curBounceCount != _maxBounceCount) ? 1 : 0;

        // 조준 할 적
        Collider2D targetEnemy = GetXthNearestEnemy(enemies, skipCount);

        // 조준 할 적이 없을 경우
        if (targetEnemy == null)
            return Vector2.zero;

        Vector2 dir = (targetEnemy.transform.position - transform.position).normalized;

        return dir;
    }

    /// <summary>
    /// 총알과 enemies에 있는 적들의 위치 차를 오름차순으로 정렬 후
    /// skipCount에 따른 순번의 적을 반환하는 함수
    /// </summary>
    /// <param name="enemies">적 배열</param>
    /// <param name="skipCount">몇 번째 가까운 적을 원하는지</param>
    /// <returns>X 번째 가까운 적</returns>
    private Collider2D GetXthNearestEnemy(Collider2D[] enemies, int skipCount)
    {
        Array.Sort(enemies, (a, b) =>
        {
            float distA = Vector2.SqrMagnitude(a.transform.position - transform.position);
            float distB = Vector2.SqrMagnitude(b.transform.position - transform.position);
            return distA.CompareTo(distB);
        });

        // 넘길 숫자가 감지한 적의 수보다 클 때 그나마 가장 가까운 대상
        if (skipCount >= enemies.Length)
            return enemies[enemies.Length - 1];

        return enemies[skipCount];
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        // 적 판단
        if(collision.gameObject.CompareTag("Minion"))
        {
            // 남은 횟수가 있을 시
            if(_curBounceCount > 0)
            {
                Debug.Log("튕김");
                // 현재 튕기는 횟수 감소
                _curBounceCount--;

                // 가장 가까운 적의 방향설정
                Vector2 dir = GetDirToEnemy();

                // 가장 가까운 적이 없을 경우 남은 _curBounceCount를 전부 소모해서 데미지를 입힘
                if(dir == Vector2.zero)
                {
                    // 현재 들어온 적 개체
                    IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();

                    // 데미지 * 데미지 배율 * 남은 튕김횟수 만큼 데미지
                    // iDamageable.TakeDamage(_model.Damage * _damageMagnification * _curBounceCount);

                    // 풀로 되돌리기
                    PoolingObject poolOb = GetComponent<PoolingObject>();
                    poolOb.ReturnToPool();
                }
                // 있을 경우 up 방향 돌리기
                else
                    transform.up = GetDirToEnemy();
            }
            // 남은 횟수가 없는 경우
            else
            {
                // 총알을 풀로 되돌리기
                PoolingObject poolOb = GetComponent<PoolingObject>();
                poolOb.ReturnToPool();
            }
        }
        // 보스 판단 시 
        else if(collision.gameObject.CompareTag("Boss"))
        {
            IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();

            // (남은 튕김횟수 - 1) * 데미지 배율로 데미지를 주고 불릿 풀로 되돌려 보내기
            // iDamageable.TakeDamage(_model.Damage * _damageMagnification * (_curBounceCount - 1));

            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }
}
