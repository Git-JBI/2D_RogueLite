using UnityEngine;

public class Bullet_Gun : BulletBase
{
    private Vector2 _dir;
    private float _moveSpeed;

    public void Init(Vector2 pos ,Vector2 dir, float movespeed)
    {
        transform.position = pos;
        transform.up = dir;
        _moveSpeed = movespeed;
        Debug.Log(dir);
        // transform.rotation = Quaternion.Euler(_dir);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * _moveSpeed * Time.fixedDeltaTime);
    }
}
