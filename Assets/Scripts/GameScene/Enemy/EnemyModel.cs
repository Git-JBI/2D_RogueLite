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

    private void Awake()
    {
        //TODO 값 따로 설정
        _maxHp = 30;
        _curHp = _maxHp;
        _moveSpeed = 1;
        _damage = 3;
    }

    public void TakeDamage(float damage)
    {
        _curHp = Mathf.Clamp(_curHp - damage, 0, _maxHp);
    }
}
