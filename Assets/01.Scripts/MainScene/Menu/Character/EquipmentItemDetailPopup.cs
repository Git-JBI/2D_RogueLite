using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 장비 아이템 세부 내용 팝업
/// </summary>
public class EquipmentItemDetailPopup : MonoBehaviour
{
    [SerializeField] private Button _outside;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _levelUpButton;
    [SerializeField] private Button _levelUpAllButton;

    private GameModel _gameModel;
    private AchievementManager _achievementManager;
    private EquipmentManager _equipmentManager;
    private EquipmentItemDatabase _dataBase;

    private int _maxLevel;

    private EquipmentItemData _equipmentItemData;
    private SO_EquipmentItemData _equipmentConfigData;
    private EquipmentItemDetailPopupViewer _popupViewer;

    public event UnityAction OnEquipped;
    public event UnityAction OnUnEquipped;
    public event UnityAction OnPopupClosed;

    private void Awake()
    {
        _gameModel = GameManager.Instance.GameModel;
        _achievementManager = GameManager.Instance.AchievementManager;
        _equipmentManager = GameManager.Instance.EquipmentManager;
        _dataBase = GameManager.Instance.EquipmentItemDatabase;

        // 팝업 외부 터치 시 팝업을 닫는 버튼 설정
        _outside.onClick.AddListener(PopupClosed);

        // 터치 시 팝업을 닫는 버튼 설정
        _closeButton.onClick.AddListener(PopupClosed);

        // 레벨 업 버튼 설정
        _levelUpButton.onClick.AddListener(ItemLevelUp);

        // 가능한 재화를 모두 사용한 레벨 업 버튼 설정
        _levelUpAllButton.onClick.AddListener(ItemLevelUpAll);
    }

    public void Init(EquipmentItemData data)
    {
        // 뷰어가 존재하지 않을 경우
        if(!_popupViewer)
            _popupViewer = GetComponentInChildren<EquipmentItemDetailPopupViewer>();

        _equipmentItemData = data;
        _equipmentConfigData = _dataBase.GetEquipmentItemData(_equipmentItemData.ItemID);

        // 아이템 레어리티에 맞는 아이템 최대 레벨
        _maxLevel = _equipmentManager.GetEquipmentItemMaxLevel(_equipmentItemData);
        
        InitViewer();
    }

    public void SetEquipmentItem()
    {
        _popupViewer.SetEquipText();
        _equipButton.onClick.AddListener(Equip);
    }

    public void SetEquippedItem()
    {
        _popupViewer.SetUnEquipText();
        _equipButton.onClick.AddListener(UnEquip);
    }

    private void InitViewer()
    {
        string itemId = _equipmentItemData.ItemID;

        RarityStatInfo statInfo = _equipmentConfigData.RaritySettings[(int)_equipmentItemData.Rarity];

        EquipmentType type  = _equipmentItemData.EquipmentType;
        Sprite itemSprite = _equipmentConfigData.IconSprite;
        Sprite raritySprite = _dataBase.GetRaritySprite(_equipmentItemData.Rarity);
        int upgradeValue = statInfo.BaseStat + (statInfo.StatPerLevel * _equipmentItemData.ItemLevel);
        string itemNameText = _dataBase.GetEquipmentItemName(itemId);
        string descText = _dataBase.GetEquipmentItemDesc(itemId);
        int goldCost = _equipmentManager.GetUpgradeGoldCost(_equipmentItemData.ItemLevel);
        int scrollCost = _equipmentManager.GetUpgradeScrollCost(_equipmentItemData.ItemLevel);

        _popupViewer.SetItemImage(itemSprite);
        _popupViewer.SetRarityImage(raritySprite);
        _popupViewer.SetItemNameText(itemNameText);
        _popupViewer.SetEquipmentType(type);
        _popupViewer.SetUpgradeValueText(upgradeValue.ToString());
        _popupViewer.SetDescText(descText);
        _popupViewer.SetItemLevelText(_equipmentItemData.ItemLevel, _maxLevel);
        _popupViewer.SetGoldCostText(_gameModel.Gold, goldCost);
        _popupViewer.SetScrollCostText(_gameModel.UpgradeScroll[(int)_equipmentItemData.EquipmentType], scrollCost);
    }

    private void Equip()
    {
        _equipmentManager.EquipItem(_equipmentItemData.InstanceID);
        PopupClosed();
    }

    private void UnEquip()
    {
        _equipmentManager.UnequipItem(_equipmentItemData.EquipmentType);
        PopupClosed();
    }

    /// <summary>
    /// 아이템 레벨업
    /// </summary>
    private void ItemLevelUp()
    {
        // 현재 아이템 레벨이 최대 레벨일 시
        if (_equipmentItemData.ItemLevel == _equipmentManager.GetEquipmentItemMaxLevel(_equipmentItemData))
            return;

        // 아이템 레벨 업
        if (_equipmentManager.ItemLevelUp(_equipmentItemData) == false)
            return;

        // 업적 갱신
        _achievementManager.Report(AchievementConditionType.EquipmentLevelUp, 1);

        // 재 표기
        RefreshLevelUpView();
    }

    /// <summary>
    /// 재화를 전부 소모한 아이템 레벨업
    /// </summary>
    private void ItemLevelUpAll()
    {
        // 현재 아이템 레벨이 최대 레벨일 시
        if (_equipmentItemData.ItemLevel == _equipmentManager.GetEquipmentItemMaxLevel(_equipmentItemData))
            return;

        // 총 레벨업
        int countLevelUp = _equipmentManager.ItemLevelUpPossible(_equipmentItemData);

        // 총 레벨업이 0일시
        if (countLevelUp == 0)
            return;

        // 업적 갱신
        _achievementManager.Report(AchievementConditionType.EquipmentLevelUp, countLevelUp);

        // 저장
        _gameModel.SaveGameData();

        // 재 표기
        RefreshLevelUpView();
    }

    /// <summary>
    /// 현재 데이터에 맞춰 정보를 재 표기하는 함수
    /// </summary>
    private void RefreshLevelUpView()
    {
        int gold = _gameModel.Gold;

        RarityStatInfo statInfo = _equipmentConfigData.RaritySettings[(int)_equipmentItemData.Rarity];
        int upgradeValue = statInfo.BaseStat + (statInfo.StatPerLevel * _equipmentItemData.ItemLevel);

        // 아이템 레벨이 최대 레벨일 시
        if (_equipmentItemData.ItemLevel == _equipmentManager.GetEquipmentItemMaxLevel(_equipmentItemData))
        {
            _popupViewer.CheckMaxLevel(true);
            // 아이템 업그레이드 벨류 표시
            _popupViewer.RefreshUpgradeValueText(upgradeValue.ToString());
            // 아이템 레벨 표시
            _popupViewer.RefreshLevelText(_equipmentItemData.ItemLevel, _maxLevel);
            // 레벨업 버튼들 숨김
            _levelUpButton.gameObject.SetActive(false);
            _levelUpAllButton.gameObject.SetActive(false);
        }

        // 아이템 레벨이 최대 레벨이 아닐 시
        else
        {
            _popupViewer.CheckMaxLevel(false);
            int scroll = _gameModel.UpgradeScroll[(int)_equipmentItemData.EquipmentType];

            int goldCost = _equipmentManager.GetUpgradeGoldCost(_equipmentItemData.ItemLevel);
            int scrollCost = _equipmentManager.GetUpgradeScrollCost(_equipmentItemData.ItemLevel);

            // 필요 골드 표시
            _popupViewer.RefreshGoldCostText(gold, goldCost);
            // 필요 스크롤 표시
            _popupViewer.RefreshScrollCostText(scroll, scrollCost);
            // 아이템 업그레이드 벨류 표시
            _popupViewer.RefreshUpgradeValueText(upgradeValue.ToString());
            // 아이템 레벨 표시
            _popupViewer.RefreshLevelText(_equipmentItemData.ItemLevel, _maxLevel);
        }
    }

    private void PopupClosed()
    {
        OnPopupClosed?.Invoke();
        _equipButton.onClick.RemoveAllListeners();

        gameObject.SetActive(false);
    }
}