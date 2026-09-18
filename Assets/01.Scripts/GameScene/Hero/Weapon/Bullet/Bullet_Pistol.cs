using UnityEngine;

public class Bullet_Pistol : PlayerWeaponObjectBase
{
    private float _moveSpeed;
    private Vector2 _startPos;

    public void Init(HeroModel model, float damage, Vector2 pos, Vector2 dir, float movespeed)
    {
        _damageMagnification = damage;
        transform.position = pos;
        _startPos = pos;
        transform.up = dir;
        _moveSpeed = movespeed;

        // 부모 설정 해제
        transform.SetParent(null);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * _moveSpeed * Time.fixedDeltaTime);

        if(Vector2.Distance(transform.position, _startPos) > 10)
        {
            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }
}
