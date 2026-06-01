using NUnit.Framework.Interfaces;
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
        
        // 골드 소모
        GameManager.Instance.GameModel.AddGold(-goldCost);

        // 재화 소모
        GameManager.Instance.GameModel.AddUpgradeScroll((int)_equipmentItemData.EquipmentType, -scrollCost);

        // 아이템 레벨업
        _equipmentItemData.ItemLevel++;

        // 보유 재료들 재설정
        gold = GameManager.Instance.GameModel.Gold;
        scroll = GameManager.Instance.GameModel.UpgradeScroll[(int)_equipmentItemData.EquipmentType];

        // 필요 재료들 재설정
        goldCost = GetUpgradeGoldCost(_equipmentItemData.ItemLevel);
        scrollCost = GetUpgradeScrollCost(_equipmentItemData.ItemLevel);

        // 소모된 재료들 재 표기(뷰어)
        _popupViewer.SetGoldCostText(goldCost, gold);
        _popupViewer.SetScrollCostText(scrollCost, scroll);

        // 아이템 레벨 재 표기(뷰어)
        SO_EquipmentItemData itemData = GameManager.Instance.EquipmentItemDatabase.GetEquipmentItemData(_equipmentItemData.ItemID);
        int maxLevel = itemData.RaritySettings[(int)_equipmentItemData.Rarity].MaxLevel;
        _popupViewer.SetItemLevelText(_equipmentItemData.ItemLevel, maxLevel);

        // 상단바 골드 텍스트 재 표기
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
