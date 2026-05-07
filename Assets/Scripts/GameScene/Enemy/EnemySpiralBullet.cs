using UnityEngine;

public class EnemySpiralBullet : MonoBehaviour
{
    private EnemyModel _enemyModel;
    private Vector2 _startDir;
    private float _bulletSpeed;
    private float _timer;

    public void Init(EnemyModel model, Vector2 startPos, Vector2 dir, float bulletSpeed)
    {
        _enemyModel = model;
        transform.position = startPos;
        transform.up = dir;
        _startDir = dir;
        _bulletSpeed = bulletSpeed;
    }

    private void FixedUpdate()
    {
        //TODO È¸Àü
        transform.Translate(Vector2.up * _bulletSpeed * Time.deltaTime);
        _timer += Time.deltaTime;

        if (_timer >= 3)
        {
            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }
}
