using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    //TODO Money텍스트 까지 적용하도록(UpgradeData 받아와서 사용) + Button_Upgrade추가 함수 필요
    [SerializeField] private GameModel _gameModel;

    private UpgradeButtonType[] _upgradeButtonTypes = { UpgradeButtonType.Damage, UpgradeButtonType.Hp, UpgradeButtonType.Armor };
    private Button_Upgrade[] _buttons;                   // 0. Damage, 1. Hp, 2. Armor

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button_Upgrade>();

        InitButtons();
    }

    private void InitButtons()
    {
        for(int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].Init(this, _upgradeButtonTypes[i]);
        }
    }

    public void Upgrade(UpgradeButtonType buttonType)
    {
        switch (buttonType)
        {
            case UpgradeButtonType.None:
                return;
            case UpgradeButtonType.Damage:
                _gameModel.IncreaseUpgradeLevel(0);
                _buttons[0].SetText(_gameModel.UpgradeLevel[0]);
                break;
            case UpgradeButtonType.Hp:
                _gameModel.IncreaseUpgradeLevel(1);
                _buttons[1].SetText(_gameModel.UpgradeLevel[1]);
                break;
            case UpgradeButtonType.Armor:
                _gameModel.IncreaseUpgradeLevel(2);
                _buttons[2].SetText(_gameModel.UpgradeLevel[2]);
                break;
        }
    }

    void UpdateButton(UpgradeButtonType buttonType)
    {

    }
}
