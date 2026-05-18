using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeManager : MonoBehaviour
{
    //TODO Money텍스트 까지 적용하도록(UpgradeData 받아와서 사용) + Button_Upgrade추가 함수 필요
    private GameModel _gameModel;

    [SerializeField]private List<UpgradeData> _upgradeDatas = new();
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

        InitButtons();
    }

    private void InitButtons()
    {
        for(int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].Init(this, _upgradeButtonTypes[i]);
            _buttons[i].SetText(_gameModel.UpgradeLevel[i], _upgradeDatas[i].Price[_gameModel.UpgradeLevel[i]]);
        }
    }

    public void Upgrade(UpgradeButtonType buttonType)
    {
        switch (buttonType)
        {
            case UpgradeButtonType.None:
                return;
            case UpgradeButtonType.Damage:
                // 가격 체크 필요
                _gameModel.IncreaseUpgradeLevel(0);
                OnUpgradeLevelChanged?.Invoke(_gameModel.Gold);
                _buttons[0].SetText(_gameModel.UpgradeLevel[0], _upgradeDatas[0].Price[_gameModel.UpgradeLevel[0]]);
                break;
            case UpgradeButtonType.Hp:
                _gameModel.IncreaseUpgradeLevel(1);
                OnUpgradeLevelChanged?.Invoke(_gameModel.Gold);
                _buttons[1].SetText(_gameModel.UpgradeLevel[1], _upgradeDatas[1].Price[_gameModel.UpgradeLevel[1]]);
                break;
            case UpgradeButtonType.Armor:
                _gameModel.IncreaseUpgradeLevel(2);
                OnUpgradeLevelChanged?.Invoke(_gameModel.Gold);
                _buttons[2].SetText(_gameModel.UpgradeLevel[2], _upgradeDatas[2].Price[_gameModel.UpgradeLevel[2]]);
                break;
        }
    }

    void UpdateButton(UpgradeButtonType buttonType)
    {

    }
}
