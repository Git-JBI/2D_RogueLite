using UnityEngine;

public class Bullet_Pistol : PlayerWeaponObjectBase
{
    private float _bulletSpeed;
    private Vector2 _startPos;

    public void Init(InGameHeroModel model, float magnification, Vector2 pos, Vector2 dir, float bulletSpeed)
    {
        base.Init(model, magnification);
        transform.position = pos;
        _startPos = pos;
        transform.up = dir;
        _bulletSpeed = bulletSpeed;

        // 부모 설정 해제
        transform.SetParent(null);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * _bulletSpeed * Time.fixedDeltaTime);

        if(Vector2.Distance(transform.position, _startPos) > 10)
        {
            ReturnToPool();
        }
    }
}
