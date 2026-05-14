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

    private float _increaseDamagePercent;
    private float _increaseMaxHpPercent;
    private float _increaseMoveSpeedPercent;

    public int Level => _level;
    public float MaxExp => _maxExp;
    public float CurExp => _curExp;
    public float MaxHp => _maxHp + _maxHp / 100 * _increaseMaxHpPercent;
    public float CurHp => _curHp;
    public float Damage => _damage + _damage / 100 * _increaseDamagePercent;
    public float MoveSpeed => _moveSpeed + _moveSpeed / 100 * _increaseMoveSpeedPercent;

    public float IncreaseDamagePercent
    {
        get
        {
            return _increaseDamagePercent;
        }
        set
        {
            _increaseDamagePercent = value;
        }
    }
    public float IncreaseMoveSpeedPercent
    {
        get
        {
            return _increaseMoveSpeedPercent;
        }
        set
        {
            _increaseMoveSpeedPercent = value;
        }
    }
    public float IncreaseMaxHpPercent
    {
        get
        {
            return _increaseMaxHpPercent;
        }
        set
        {
            _increaseMaxHpPercent = value;
            _curHp = _curHp + _curHp / 100 * _increaseMaxHpPercent;
        }
    }

    public bool IsAlive => _curHp > 0;

    public event UnityAction<int> OnChangeLevel;
    public event UnityAction<float, float> OnChangeHp;
    public event UnityAction<float, float> OnChangeExp;

    private void Awake()
    {
        // TODO 추후 싱글톤 사용한 플레이어 스텟에서 값 초기화
        _maxHp = 1000;
        _curHp = _maxHp;
        _damage = 10;
        _moveSpeed = 2;
        _maxExp = 100;
        _curExp = 0;
        _level = 1;

        SetHeroBonusData();
    }

    public void Init()
    {
        OnChangeExp?.Invoke(_curExp, _maxExp);
        OnChangeLevel?.Invoke(_level);
    }

    /// <summary>
    /// 캐릭터마다 다른 보너스 능력치 설정
    /// </summary>
    private void SetHeroBonusData()
    {
        switch(GameManager.Instance.GameModel.HeroData.StatData.StatType)
        {
            case HeroStatType.None:
                break;
            case HeroStatType.Damage:
                _damage += GameManager.Instance.GameModel.HeroData.GetStatValue();
                break;
            case HeroStatType.MoveSpeed:
                _moveSpeed += GameManager.Instance.GameModel.HeroData.GetStatValue();
                break;
            case HeroStatType.MaxHp:
                _maxHp += GameManager.Instance.GameModel.HeroData.GetStatValue();
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        _curHp = Mathf.Clamp(_curHp - damage, 0, MaxHp);
        OnChangeHp?.Invoke(_curHp, MaxHp);
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
