using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChasingBullet : PlayerBulletBase
{
    private float _bulletSpeed;
    private int _maxBounceCount;
    private int _curBounceCount;
    private Vector2 _startPos;
    private Vector2 _moveDir;

    private LayerMask _enemyMask;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * _bulletSpeed * Time.fixedDeltaTime);
    }

    public void Init(HeroModel model, float damage,float bulletSpeed, int bounceCount, Vector2 pos)
    {
        _model = model;
        _damageMagnification = damage;
        _bulletSpeed = bulletSpeed;
        _maxBounceCount = bounceCount;
        _curBounceCount = _maxBounceCount;
        _startPos = pos;



        // 레이어마스크 값이 없을 시 초기화
        if (_enemyMask.value == 0)
            _enemyMask = LayerMask.GetMask("Enemy");
    }

    private Vector2 SetBulletDir()
    {
        // 적 리스트
        List<Collider2D> enemies = new();
        // 적과 총알 사이의 거리 Dictionary
        Dictionary<float, int> distanceMap = new();

        enemies = Physics2D.OverlapCircleAll(transform.position, 20, _enemyMask).ToList();

        // 발견된 적이 있을 경우
        if (enemies.Count > 0)
        {
            // 발견된 적들 순회
            for (int i = 0; i < enemies.Count; i++)
            {
                // 발견된 적과 총알 사이의 거리
                float distance = Vector2.Distance(transform.position, enemies[i].transform.position);
                // Dic 추가
                distanceMap.Add(distance, i);
            }

            // 처음 발사 시점에는 가장 가까운 적으로
            if (_maxBounceCount == _curBounceCount)
            {
                int index;
                Collider2D enemy;
                return Vector2.zero;
            }
            // 한번 튕긴 이후로는 가장 가까운 적 다음 적으로
            else
            {

                return Vector2.zero;
            }
        }
        // 발견된 적이 없는 경우
        else
            return Vector2.zero;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        // 적 판단
        if(collision.gameObject.CompareTag("Enemy"))
        {
            // 남은 횟수가 있을 시
            // 방향 재설정
        }
        // 보스 판단 시 (남은 튕김횟수 - 1) * 데미지 배율로 데미지를 주고 불릿 풀로 되돌려 보내기
        else if(collision.gameObject.CompareTag("Boss"))
        {

        }
    }
}
