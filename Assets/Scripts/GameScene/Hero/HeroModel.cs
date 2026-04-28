using UnityEngine;
using UnityEngine.Events;

public class HeroModel : MonoBehaviour
{
    private int _level;
    private float _maxExp;
    private float _curExp;
    private float _maxHp;
    private float _curHp;
    private float _damage;
    private float _moveSpeed;

    public int Level => _level;
    public float MaxExp => _maxExp;
    public float CurExp => _curExp;
    public float CurHp => _curHp;
    public float Damage => _damage;
    public float MoveSpeed => _moveSpeed;
    public bool IsAlive => _curHp > 0;

    public event UnityAction<int> OnChangeLevel;
    public event UnityAction<float, float> OnChangeHp;
    public event UnityAction<float, float> OnChangeExp;

    private void Awake()
    {
        // TODO 추후 싱글톤 사용한 플레이어 스텟에서 값 초기화
        _maxHp = 50;
        _curHp = _maxHp;
        _damage = 5;
        _moveSpeed = 2;
        _maxExp = 100;
        _curExp = 0;
        _level = 1;
    }

    public void TakeDamage(float damage)
    {
        _curHp = Mathf.Clamp(_curHp - damage, 0, _maxHp);
        OnChangeHp?.Invoke(_curHp, _maxHp);
    }

    public void IncreaseExp(int exp)
    {
        _curExp += exp;
        
        if(_curExp >= _maxExp)
        {
            _level++;
            _curExp -= _maxExp;

            OnChangeLevel?.Invoke(_level);
        }
        OnChangeExp?.Invoke(_curExp, _maxExp);
    }
}
