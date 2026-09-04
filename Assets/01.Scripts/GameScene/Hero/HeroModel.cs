using UnityEngine;
using UnityEngine.Events;

public class HeroModel : MonoBehaviour
{
    private UpgradeModel _upgradeModel;
    private SO_ExpConfigData _expConfigData;

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

    public float MaxHp => (_maxHp + _upgradeHp) * (1 + _passiveMaxHpPercent / 100);
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

    /// <summary>
    /// T0 : 현재 레벨
    /// </summary>
    public event UnityAction<int> OnChangeLevel;
    /// <summary>
    /// T0 : 현재 Hp, T1 : 최대 Hp
    /// </summary>
    public event UnityAction<float, float> OnChangeHp;
    /// <summary>
    /// T0 : 현재 Exp, T1 : 최대 Exp
    /// </summary>
    public event UnityAction<float, float> OnChangeExp;

    private void Awake()
    {
        _upgradeModel = GameManager.Instance.GameModel.UpgradeModel;
        _expConfigData = GameManager.Instance.ResourceManager.GetScriptable("GameDatas/HeroConfigData/ExpConfigData/ExpConfigData") as SO_ExpConfigData;

        // TODO 추후 싱글톤 사용한 플레이어 스텟에서 값 초기화 >>>>> 필요없는 변수들이 있는 것 같음 기본값을 GameModel에서 설정 후 기본 값 설정 후 인게임 업그레이드 데이터를 적용하는 방식
        _level = 1;
        _maxHp = 10000;
        _curHp = _maxHp;
        _damage = 10;
        _moveSpeed = 2;
        _maxExp = _expConfigData.GetMaxExp(_level);
        _curExp = 0;

        _upgradeDamage = _upgradeModel.GetValue(UpgradeType.Damage);
        _upgradeHp = _upgradeModel.GetValue(UpgradeType.Hp);
        _upgradeArmor = _upgradeModel.GetValue(UpgradeType.Armor);

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

    /// <summary>
    /// 체력을 회복하는 함수
    /// </summary>
    /// <param name="value">더해지는 체력의 양</param>
    public void HealHp(float value)
    {
        float heal = value;

        // _curHp는 _maxHp을 초과할 수 없음
        _curHp = Mathf.Min(_curHp + heal, _maxHp);
    }

    /// <summary>
    /// 데미지를 입는 함수
    /// </summary>
    /// <param name="value">입는 데미지</param>
    public void TakeDamage(float value)
    {
        float damage = value;

        // _curHp는 0미만이 될 수 없음
        _curHp = Mathf.Max(_curHp - damage, 0);

        Debug.Log($"Hp : {_curHp}");
    }

    /// <summary>
    /// 경험치 증가 함수
    /// </summary>
    /// <param name="exp"></param>
    public void IncreaseExp(int exp)
    {
        _curExp += exp;

        CheckExp();

        OnChangeExp?.Invoke(_curExp, _maxExp);
    }

    /// <summary>
    /// 현재 경험치가 최대 경험치보다 큰지 확인용도 함수 
    /// </summary>
    public void CheckExp()
    {
        if(_curExp >= _maxExp)
        {
            // 레벨 증가
            _level++;

            // 현재 경험치에서 최대 경험치만큼 감소
            _curExp -= _maxExp;

            // 레벨에 따른 최대 경험치 재설정
            _maxExp = _expConfigData.GetMaxExp(_level);

            // 레벨업 시 이벤트 발행
            OnChangeLevel?.Invoke(_level);
        }
    }
}