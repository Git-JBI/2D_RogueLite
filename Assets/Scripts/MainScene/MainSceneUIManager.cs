using UnityEngine;

public class MainSceneUIManager : MonoBehaviour
{
    private UI_Text_Gold _goldText;

    private HeroSelectViewer _heroSelectViewer;
    private MenuController _menuController;
    private UpgradeManager _upgradeManager;

    private void Awake()
    {
        _goldText = GetComponentInChildren<UI_Text_Gold>();

        _heroSelectViewer = GetComponentInChildren<HeroSelectViewer>();
        _menuController = GetComponentInChildren<MenuController>();
        _upgradeManager = GetComponentInChildren<UpgradeManager>();

        Init();

        _upgradeManager.OnUpgradeLevelChanged += _goldText.UpdateGoldText;
    }

    private void Init()
    {
        _goldText.UpdateGoldText(GameManager.Instance.GameModel.Gold);
    }
}