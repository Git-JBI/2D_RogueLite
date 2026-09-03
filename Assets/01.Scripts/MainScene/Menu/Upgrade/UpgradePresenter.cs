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
        _viewer.OnDamageUpgradeClicked += DamageUpgrade;
        _viewer.OnHpUpgradeClicked += HpUpgrade;
        _viewer.OnArmorUpgradeClicked += ArmorUpgrade;
    }

    /// <summary>
    /// 뷰어 초기화
    /// </summary>
    private void InitViewer()
    {
        _viewer.SetDamageButtonText(_model.GetDamageUpgradeLevel(), _model.GetDamageUpgradePrice());
        _viewer.SetHpButtonText(_model.GetHpUpgradeLevel(), _model.GetHpUpgradePrice());
        _viewer.SetArmorButtonText(_model.GetArmorUpgradeLevel(), _model.GetArmorUpgradePrice());
    }
    

    private void DamageUpgrade()
    {
        // 업그레이드 실행 반환 타입
        UpgradeReturnType type = _model.DamageUpgradeLevelUp();

        switch(type)
        {
            // 최대 레벨
            case UpgradeReturnType.MaxLevel:
                _viewer.PlayMaxLevelWarning();
                break;
            // 골드 부족
            case UpgradeReturnType.NoGold:
                _viewer.PlayNotEnoughGoldWarning();
                break;
            // 업그레이드 실행
            case UpgradeReturnType.Success:
                _viewer.SetDamageButtonText(_model.GetDamageUpgradeLevel(), _model.GetDamageUpgradePrice());
                break;
        }
    }
    private void HpUpgrade()
    {
        // 업그레이드 실행 반환 타입
        UpgradeReturnType type = _model.HpUpgradeLevelUp();

        switch (type)
        {
            // 최대 레벨
            case UpgradeReturnType.MaxLevel:
                _viewer.PlayMaxLevelWarning();
                break;
            // 골드 부족
            case UpgradeReturnType.NoGold:
                _viewer.PlayNotEnoughGoldWarning();
                break;
            // 업그레이드 실행
            case UpgradeReturnType.Success:
                _viewer.SetHpButtonText(_model.GetHpUpgradeLevel(), _model.GetHpUpgradePrice());
                break;
        }
    }
    private void ArmorUpgrade()
    {
        // 업그레이드 실행 반환 타입
        UpgradeReturnType type = _model.ArmorUpgradeLevelUp();

        switch (type)
        {
            // 최대 레벨
            case UpgradeReturnType.MaxLevel:
                _viewer.PlayMaxLevelWarning();
                break;
            // 골드 부족
            case UpgradeReturnType.NoGold:
                _viewer.PlayNotEnoughGoldWarning();
                break;
            // 업그레이드 실행
            case UpgradeReturnType.Success:
                _viewer.SetArmorButtonText(_model.GetArmorUpgradeLevel(), _model.GetArmorUpgradePrice());
                break;
        }
    }
}
