using UnityEngine;

public class Bullet_Gun : BulletBase
{
    private Vector2 _dir;
    private float _moveSpeed;

    public void Init(Vector2 dir, float movespeed)
    {
        _dir = dir;
        _moveSpeed = movespeed;
    }

    private void FixedUpdate()
    {
        transform.Translate(_dir * _moveSpeed);
    }
}
