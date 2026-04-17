using UnityEngine;

public class HeroModel : MonoBehaviour
{
    private float _maxHp;
    private float _curHp;
    private float _damage;
    private float _moveSpeed;

    public float CurHp => _curHp;
    public float Damage => _damage;
    public float MoveSpeed => _moveSpeed;

    public bool IsAlive => _curHp > 0;

    private void Awake()
    {
        // TODO 추후 싱글톤 사용한 플레이어 스텟에서 값 초기화
        _maxHp = 50;
        _curHp = _maxHp;
        _damage = 5;
        _moveSpeed = 2;
    }

    public void TakeDamage(float damage)
    {
        _curHp = Mathf.Clamp(_curHp - damage, 0, _maxHp);
        Debug.Log($"현재 체력 : {_curHp}");
    }
}
