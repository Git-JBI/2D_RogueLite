using UnityEngine;

public class UpgradePresenter : MonoBehaviour
{
    [SerializeField] private MainSceneUpgradeViewer _viewer;
    private UpgradeModel _model;

    private void Awake()
    {
        _model = GameManager.Instance.GameModel.UpgradeModel;

        InitViewer();
    }

    private void Start()
    {
        _viewer.OnUpgradeClicked += HandleUpgrade;
    }

    private void OnDestroy()
    {
        _viewer.OnUpgradeClicked -= HandleUpgrade;
    }

    /// <summary>
    /// 뷰어 초기화
    /// </summary>
    private void InitViewer()
    {
        Refresh(UpgradeType.Damage);
        Refresh(UpgradeType.Hp);
        Refresh(UpgradeType.Armor);
    }
    
    private void HandleUpgrade(UpgradeType type)
    {
        UpgradeReturnType result = _model.TryUpgrade(type);

        switch(result)
        {
            case UpgradeReturnType.MaxLevel:
                _viewer.PlayMaxLevelWarning();
                break;
            case UpgradeReturnType.NoGold:
                _viewer.PlayNotEnoughGoldWarning();
                break;
            case UpgradeReturnType.Success:
                Refresh(type);
                break;
        }
    }

    private void Refresh(UpgradeType type)
    {
        _viewer.SetButtonText(type, _model.GetLevel(type), _model.GetPrice(type));
    }
}
