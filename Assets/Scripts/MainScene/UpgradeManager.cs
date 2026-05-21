using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeManager : MonoBehaviour
{
    private int _maxUpgradeLevel;
    //TODO Money텍스트 까지 적용하도록(UpgradeData 받아와서 사용) + Button_Upgrade추가 함수 필요
    private GameModel _gameModel;

    [SerializeField] private List<UpgradeData> _upgradeDatas = new();
    private UpgradeButtonType[] _upgradeButtonTypes = { UpgradeButtonType.Damage, UpgradeButtonType.Hp, UpgradeButtonType.Armor };
    [SerializeField] private Button_Upgrade[] _buttons;                   // 0. Damage, 1. Hp, 2. Armor

    public event UnityAction<int> OnUpgradeLevelChanged;

    private void Awake()
    {
        _gameModel = GameManager.Instance.GameModel;

        _buttons = GetComponentsInChildren<Button_Upgrade>();

        _upgradeDatas.Add(Resources.Load<UpgradeData>("GameDatas/UpgradeData/UpgradeDataDamage"));
        _upgradeDatas.Add(Resources.Load<UpgradeData>("GameDatas/UpgradeData/UpgradeDataHp"));
        _upgradeDatas.Add(Resources.Load<UpgradeData>("GameDatas/UpgradeData/UpgradeDataArmor"));

        _maxUpgradeLevel = _upgradeDatas[0].Price.Length;

        InitButtons();
    }

    private void InitButtons()
    {
        for(int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].Init(this, _upgradeButtonTypes[i]);
            if (_gameModel.UpgradeLevel[i] == _maxUpgradeLevel)
                _buttons[i].SetMaxLevel();
            else
                _buttons[i].SetText(_gameModel.UpgradeLevel[i], _upgradeDatas[i].Price[_gameModel.UpgradeLevel[i]]);
        }
    }

    public void Upgrade(UpgradeButtonType buttonType)
    {
        switch (buttonType)
        {
            case UpgradeButtonType.None:
                return;
            case UpgradeButtonType.Damage:          // 데미지 업그레이드
                // 최대 레벨 도달했을 경우 리턴
                if (_gameModel.UpgradeLevel[0] == _maxUpgradeLevel)
                    return;
                // 골드 부족할 경우 리턴
                if (_upgradeDatas[0].Price[_gameModel.UpgradeLevel[0]] > _gameModel.Gold)
                    return;
                _gameModel.AddGold(-_upgradeDatas[0].Price[_gameModel.UpgradeLevel[0]]);
                _gameModel.IncreaseUpgradeLevel(0);
                OnUpgradeLevelChanged?.Invoke(_gameModel.Gold);
                if (_gameModel.UpgradeLevel[0] == _maxUpgradeLevel)
                    // 최대 레벨 도달 텍스트 표시
                    _buttons[0].SetMaxLevel();
                else
                    _buttons[0].SetText(_gameModel.UpgradeLevel[0], _upgradeDatas[0].Price[_gameModel.UpgradeLevel[0]]);
                break;
            case UpgradeButtonType.Hp:              // Hp 업그레이드
                // 최대 레벨 도달했을 경우 리턴
                if (_gameModel.UpgradeLevel[1] == _maxUpgradeLevel)
                    return;
                if (_upgradeDatas[1].Price[_gameModel.UpgradeLevel[1]] > _gameModel.Gold)
                    return;
                _gameModel.AddGold(-_upgradeDatas[1].Price[_gameModel.UpgradeLevel[1]]);
                _gameModel.IncreaseUpgradeLevel(1);
                OnUpgradeLevelChanged?.Invoke(_gameModel.Gold);
                if (_gameModel.UpgradeLevel[1] == _maxUpgradeLevel)
                    // 최대 레벨 도달 텍스트 표시
                    _buttons[1].SetMaxLevel();
                else
                    _buttons[1].SetText(_gameModel.UpgradeLevel[1], _upgradeDatas[1].Price[_gameModel.UpgradeLevel[1]]);
                break;
            case UpgradeButtonType.Armor:           // Armor 업그레이드
                // 최대 레벨 도달했을 경우 리턴
                if (_gameModel.UpgradeLevel[2] == _maxUpgradeLevel)
                    return;
                if (_upgradeDatas[2].Price[_gameModel.UpgradeLevel[2]] > _gameModel.Gold)
                    return;
                _gameModel.AddGold(-_upgradeDatas[2].Price[_gameModel.UpgradeLevel[2]]);
                _gameModel.IncreaseUpgradeLevel(2);
                OnUpgradeLevelChanged?.Invoke(_gameModel.Gold);
                if (_gameModel.UpgradeLevel[2] == _maxUpgradeLevel)
                    // 최대 레벨 도달 텍스트 표시
                    _buttons[2].SetMaxLevel();
                else
                    _buttons[2].SetText(_gameModel.UpgradeLevel[2], _upgradeDatas[2].Price[_gameModel.UpgradeLevel[2]]);
                break;
        }
    }


}
