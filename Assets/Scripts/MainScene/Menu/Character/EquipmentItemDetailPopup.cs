using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 장비 아이템 세부 내용 팝업
/// </summary>
public class EquipmentItemDetailPopup : MonoBehaviour
{
    //TODO 추후 MainSceneUIManager에서
    [SerializeField] private UI_Text_Gold _upperGoldText;
    [SerializeField] private Button _outside;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _levelUpButton;
    [SerializeField] private Button _levelUpAllButton;

    private int _maxLevel;

    private EquipmentItemData _equipmentItemData;
    private EquipmentItemDetailPopupViewer _popupViewer;
    private SO_EquipmentUpgredeTable _upgradeTable;

    private Dictionary<int, LevelUpCost> _upgradeTableMap = new();

    public event UnityAction OnEquipped;
    public event UnityAction OnUnEquipped;
    public event UnityAction OnPopupClosed;

    private void Awake()
    {
        _outside.onClick.AddListener(PopupClosed);
        _closeButton.onClick.AddListener(PopupClosed);
        _levelUpButton.onClick.AddListener(ItemLevelUp);
        _levelUpAllButton.onClick.AddListener(ItemLevelUpAll);
    }

    public void Init(EquipmentItemData data)
    {
        // 뷰어가 존재하지 않을 경우
        if(!_popupViewer)
            _popupViewer = GetComponentInChildren<EquipmentItemDetailPopupViewer>();

        // 업그레이드 테이블 데이터가 null일 경우
        if(_upgradeTable == null)
        {
            _upgradeTable = Resources.Load<SO_EquipmentUpgredeTable>("GameDatas/EquipmentUpgradeData/EquipmentUpgradeData");

            Debug.Log(_upgradeTable.CostList.Count);
        }

        // 업그레이드 테이블 맵의 길이가 0일 경우
        if(_upgradeTableMap.Count == 0)
        {
            foreach (LevelUpCost level in _upgradeTable.CostList)
            {
                // if(_upgradeTableMap.ContainsKey(level.Level))
                // {
                //     Debug.Log($"중복된 레벨 데이터 Lv.{level.Level}이 있습니다.");
                //     continue;
                // }
                _upgradeTableMap.Add(level.Level, level);
            }
        }

        _equipmentItemData = data;

        SO_EquipmentItemData itemData = GameManager.Instance.EquipmentItemDatabase.GetEquipmentItemData(_equipmentItemData.ItemID);
        
        // 아이템 이름
        string itemName = itemData.ItemName;
        // 아이템 설명
        string itemDesc = itemData.Desc;
        // 아이템 레어리티에 맞는 아이템 최대 레벨
        _maxLevel = itemData.RaritySettings[(int)_equipmentItemData.Rarity].MaxLevel;
        int goldCost = GetUpgradeGoldCost(_equipmentItemData.ItemLevel);
        int scrollCost = GetUpgradeScrollCost(_equipmentItemData.ItemLevel);

        _popupViewer.SetItemLevelText(_equipmentItemData.ItemLevel, _maxLevel);
        _popupViewer.Init(itemName,_equipmentItemData.EquipmentType, _equipmentItemData.Rarity, _equipmentItemData.RarityLevel, itemDesc, goldCost, scrollCost);
        RefreshLevelUpView();
    }

    public void SetEquip()
    {
        _popupViewer.SetEquipText();
        _equipButton.onClick.AddListener(Equip);
    }

    public void SetUnEquip()
    {
        _popupViewer.SetUnEquipText();
        _equipButton.onClick.AddListener(UnEquip);
    }

    private void Equip()
    {
        GameManager.Instance.GameModel.EquipItem(_equipmentItemData.InstanceID);
        PopupClosed();
    }

    private void UnEquip()
    {
        GameManager.Instance.GameModel.UnequipItem(_equipmentItemData.EquipmentType);
        PopupClosed();
    }

    /// <summary>
    /// 아이템 레벨업
    /// </summary>
    private void ItemLevelUp()
    {
        int gold = GameManager.Instance.GameModel.Gold;
        int scroll = GameManager.Instance.GameModel.UpgradeScroll[(int)_equipmentItemData.EquipmentType];
        int goldCost = GetUpgradeGoldCost(_equipmentItemData.ItemLevel);
        int scrollCost = GetUpgradeScrollCost(_equipmentItemData.ItemLevel);
        // 현재 가능한 최대 레벨일 경우 리턴
        if (_equipmentItemData.ItemLevel == _maxLevel)
            return;

        // 필요 골드, 필요 스크롤 파악 부족하면 리턴
        if (gold < goldCost || scroll < scrollCost)
            return;

        // 아이템 레벨업
        GameManager.Instance.GameModel.UpgradeEquipmentItemLevel(goldCost, (int)_equipmentItemData.EquipmentType, scrollCost);

        // 업적 갱신
        GameManager.Instance.AchievementManager.Report(AchievementConditionType.EquipmentLevelUp, 1);

        // 재 표기
        RefreshLevelUpView();
    }

    /// <summary>
    /// 재화를 전부 소모한 아이템 레벨업
    /// </summary>
    private void ItemLevelUpAll()
    {
        // 현재 보유한 골드
        int gold = GameManager.Instance.GameModel.Gold;
        // 현재 선택된 장비 타입에 따른 스크롤 갯수
        int scroll = GameManager.Instance.GameModel.UpgradeScroll[(int)_equipmentItemData.EquipmentType];

        // 레벨업 한 횟수
        int countLevelUp = 0;
        // 총 사용한 골드
        int totalGoldCost = 0;
        // 총 사용한 스크롤
        int totalScrollCost = 0;

        //TODO 추후 수정 필요 범위 관련 or maxLevel값 다 채우기
        while (_equipmentItemData.ItemLevel < _maxLevel)
        {
            int goldCost = GetUpgradeGoldCost(_equipmentItemData.ItemLevel);
            int scrollCost = GetUpgradeScrollCost(_equipmentItemData.ItemLevel);

            if (goldCost <= 0 && scrollCost <= 0)
                break;

            // 보유한 골드, 스크롤이 사용할 재화보다 적은 경우
            if (gold < goldCost || scroll < scrollCost)
                break;

            gold -= goldCost;
            scroll -= scrollCost;

            totalGoldCost += goldCost;
            totalScrollCost += scrollCost;

            _equipmentItemData.ItemLevel++;
            countLevelUp++;
            
        }
        // 업적 갱신
        GameManager.Instance.AchievementManager.Report(AchievementConditionType.EquipmentLevelUp, countLevelUp);

        // 사용한 재료들 데이터 저장(레벨은 바로 저장되기에 따로할 필요 없음)
        GameManager.Instance.GameModel.UpgradeEquipmentItemLevel(totalGoldCost, (int)_equipmentItemData.EquipmentType, totalScrollCost);

        // 재 표기
        RefreshLevelUpView();
    }

    /// <summary>
    /// 현재 데이터에 맞춰 정보를 재 표기하는 함수
    /// </summary>
    private void RefreshLevelUpView()
    {
        int gold = GameManager.Instance.GameModel.Gold;
        SO_EquipmentItemData equipmentItemData = GameManager.Instance.EquipmentItemDatabase.GetEquipmentItemData(_equipmentItemData.ItemID);

        // 아이템 레벨이 최대 레벨일 시
        if (_equipmentItemData.ItemLevel == equipmentItemData.RaritySettings[(int)_equipmentItemData.Rarity].MaxLevel)
        {
            _popupViewer.isMaxLevel(true);
            // 아이템 레벨 표시
            _popupViewer.SetItemLevelText(_equipmentItemData.ItemLevel, _maxLevel);
            // 레벨업 버튼들 숨김
            _levelUpButton.gameObject.SetActive(false);
            _levelUpAllButton.gameObject.SetActive(false);
        }

        // 아이템 레벨이 최대 레벨이 아닐 시
        else
        {
            _popupViewer.isMaxLevel(false);
            int scroll = GameManager.Instance.GameModel.UpgradeScroll[(int)_equipmentItemData.EquipmentType];

            int goldCost = GetUpgradeGoldCost(_equipmentItemData.ItemLevel);
            int scrollCost = GetUpgradeScrollCost(_equipmentItemData.ItemLevel);

            _popupViewer.SetGoldCostText(gold, goldCost);
            _popupViewer.SetScrollCostText(scroll, scrollCost);
            // 아이템 레벨 표시
            _popupViewer.SetItemLevelText(_equipmentItemData.ItemLevel, _maxLevel);
            // 레벨업 버튼들 표시
            _levelUpButton.gameObject.SetActive(true);
            _levelUpAllButton.gameObject.SetActive(true);
        }

        // 상단바 골드 표시
        _upperGoldText.UpdateGoldText(gold);
    }

    /// <summary>
    /// 아이템 레벨에 따른 필요한 골드 코스트를 가져오는 함수
    /// </summary>
    /// <param name="level">아이템 레벨</param>
    /// <returns></returns>
    private int GetUpgradeGoldCost(int level)
    {
        if(_upgradeTableMap.TryGetValue(level, out LevelUpCost cost))
            return cost.GoldCost;
        Debug.Log($"{level}에 대한 데이터가 없습니다.");
        return 0;
    }

    /// <summary>
    /// 아이템 레벨에 따른 필요한 스크롤을 가져오는 함수
    /// </summary>
    /// <param name="level">아이템 레벨</param>
    /// <returns></returns>
    private int GetUpgradeScrollCost(int level)
    {
        if (_upgradeTableMap.TryGetValue(level, out LevelUpCost cost))
            return cost.ScrollCost;
        Debug.Log($"{level}에 대한 데이터가 없습니다.");
        return 0;
    }

    private void PopupClosed()
    {
        OnPopupClosed?.Invoke();
        _equipButton.onClick.RemoveAllListeners();

        gameObject.SetActive(false);
    }
}
