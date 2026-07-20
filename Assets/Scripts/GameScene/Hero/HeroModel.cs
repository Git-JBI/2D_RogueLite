using UnityEngine;
using UnityEngine.Events;

public class HeroModel : MonoBehaviour
{
    private UpgradeModel _upgradeModel;

    private int _level;
    private float _maxExp;
    private float _curExp;
    private float _maxHp;
    private float _curHp;
    private float _damage;
    private float _moveSpeed;
    private float _armor;

    private float _upgradeDamage;
    private float _upgradeHp;
    private float _upgradeArmor;

    private float _passiveDamagePercent;
    private float _passiveMaxHpPercent;
    private float _passiveMoveSpeedPercent;

    public int Level => _level;
    public float MaxExp => _maxExp;
    public float CurExp => _curExp;
    public float MaxHp => (_maxHp + _upgradeHp) * (1 + _passiveMaxHpPercent / 100);
    public float CurHp => _curHp;
    public float Damage => (_damage + _upgradeDamage) * (1 + _passiveDamagePercent / 100);
    public float MoveSpeed => _moveSpeed * (1 + _passiveMoveSpeedPercent / 100);
    public float Armor => _armor + _upgradeArmor;

    public float PassiveDamagePercent
    {
        get
        {
            return _passiveDamagePercent;
        }
        set
        {
            _passiveDamagePercent = value;
        }
    }
    public float PassiveMoveSpeedPercent
    {
        get
        {
            return _passiveMoveSpeedPercent;
        }
        set
        {
            _passiveMoveSpeedPercent = value;
        }
    }
    public float PassiveMaxHpPercent
    {
        get
        {
            return _passiveMaxHpPercent;
        }
        set
        {
            _passiveMaxHpPercent = value;
            _curHp = _curHp + _curHp / 100 * _passiveMaxHpPercent;
        }
    }

    public bool IsAlive => _curHp > 0;

    public event UnityAction<int> OnChangeLevel;
    public event UnityAction<float, float> OnChangeHp;
    public event UnityAction<float, float> OnChangeExp;

    private void Awake()
    {
        _upgradeModel = GameManager.Instance.GameModel.UpgradeModel;

        // TODO 추후 싱글톤 사용한 플레이어 스텟에서 값 초기화 >>>>> 필요없는 변수들이 있는 것 같음 기본값을 GameModel에서 설정 후 기본 값 설정 후 인게임 업그레이드 데이터를 적용하는 방식
        _maxHp = 10000;
        _curHp = _maxHp;
        _damage = 10;
        _moveSpeed = 2;
        _maxExp = 100;
        _curExp = 0;
        _level = 1;

        _upgradeDamage = _upgradeModel.GetUpgradeDamageValue();
        _upgradeHp = _upgradeModel.GetUpgradeHpValue();
        _upgradeArmor = _upgradeModel.GetUpgradeArmorValue();

        SetHeroBonusData();
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