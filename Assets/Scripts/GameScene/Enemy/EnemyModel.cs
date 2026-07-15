using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    private float _maxHp;
    private float _curHp;
    private float _moveSpeed;
    private float _damage;

    public float CurHp => _curHp;
    public float MoveSpeed => _moveSpeed;
    public float Damage => _damage;

    public EnemyModel(float maxHp, float curHp, float moveSpeed, float damage, int mul)
    {
        _maxHp = maxHp;
        _curHp = curHp;
        _moveSpeed = moveSpeed;
        _damage = damage;
    }

    public void TakeDamage(float damage)
    {
        _curHp = Mathf.Clamp(_curHp - damage, 0, _maxHp);
    }
}
