using UnityEngine;

public class MainSceneUIManager : MonoBehaviour
{
    /// <summary>
    /// 상단 골드 텍스트
    /// </summary>
    [SerializeField] private UI_Text_Gold _goldText;

    private MenuController _menuController;
    private UpgradeManager _upgradeManager;

    private void Awake()
    {
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