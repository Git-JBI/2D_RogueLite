using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private int _equipmentBonusAtk;
    private int _equipmentBonusHp;

    private GameModel _gameModel;
    private EquipmentModel _equipmentModel;

    private SO_EquipmentUpgredeTable _upgradeTable;
    private Dictionary<int, LevelUpCost> _upgradeTableMap = new();

    /// <summary>
    /// 장비 시 보너스 공격력
    /// </summary>
    public int EquipmentBonusAtk
    {
        get
        {
            _equipmentBonusAtk = 0;

            // 장착중인 아이템 리스트 순회
            foreach(EquipmentItemData data in GameManager.Instance.GameModel.EquipmentModel.EquippedItems)
            {
                // 아이템 설정 데이터
                SO_EquipmentItemData itemData = GameManager.Instance.EquipmentItemDatabase.GetEquipmentItemData(data.ItemID);

                // 장비 스텟 타입이 공격력인 경우
                if(itemData.EquipmentStatType == EquipmentStatType.Attack)
                {
                    // 현재 레어리티에 맞는 베이스스텟, 레벨당 스텟 불러오기
                    RarityStatInfo info = itemData.RaritySettings.Find(x => x.Rarity == data.Rarity);

                    // 베이스 스텟 + 레벨 당 스텟 * 아이템 레벨
                    _equipmentBonusAtk = _equipmentBonusAtk + info.BaseStat + info.StatPerLevel * data.ItemLevel;
                }
            }
            return _equipmentBonusAtk;
        }
    }
    /// <summary>
    /// 장비 시 보너스 체력
    /// </summary>
    public int EquipmentBonusHp
    {
        get
        {
            _equipmentBonusHp = 0;

            // 장착중인 아이템 리스트 순회
            foreach (EquipmentItemData data in GameManager.Instance.GameModel.EquipmentModel.EquippedItems)
            {
                // 아이템 설정 데이터
                SO_EquipmentItemData itemData = GameManager.Instance.EquipmentItemDatabase.GetEquipmentItemData(data.ItemID);

                // 장비 스텟 타입이 Hp인 경우
                if (itemData.EquipmentStatType == EquipmentStatType.Hp)
                {
                    // 현재 레어리티에 맞는 베이스스텟, 레벨당 스텟 불러오기
                    RarityStatInfo info = itemData.RaritySettings.Find(x => x.Rarity == data.Rarity);

                    // 베이스 스텟 + 레벨 당 스텟 * 아이템 레벨
                    _equipmentBonusHp = _equipmentBonusHp + info.BaseStat + info.StatPerLevel * data.ItemLevel;
                }
            }
            return _equipmentBonusHp;
        }
    }

    private void Awake()
    {
        _upgradeTable = Resources.Load<SO_EquipmentUpgredeTable>("GameDatas/EquipmentUpgradeData/EquipmentUpgradeData");

        foreach(LevelUpCost level in _upgradeTable.CostList)
        {
            _upgradeTableMap.Add(level.Level, level);
        }
    }

    public void Init(GameModel gameModel)
    {
        _gameModel = gameModel;
        _equipmentModel = gameModel.EquipmentModel;
    }

    /// <summary>
    /// 장비 장착
    /// </summary>
    /// <param name="instanceId"></param>
    public void EquipItem(string instanceId)
    {
        // 장비 인벤토리에서 장착할 아이템을 찾기
        EquipmentItemData item = _equipmentModel.GetEquipmentItem(instanceId);

        // 장착할 아이템의 타입
        EquipmentType type = item.EquipmentType;

        // 장착 중인 아이템
        EquipmentItemData equippedItem = _equipmentModel.GetEquippedItem(type);

        // 장착 중인 아이템이 있을 시
        if(equippedItem != null)
        {
            // 장착중인 장비 리스트에서 장착중인 장비 제거
            _equipmentModel.RemoveEquippedItemList(equippedItem);
            // 보유중인 장비 리스트에 장착중이였던 장비 추가
            _equipmentModel.AddEquipmentList(equippedItem);
        }

        // 보유중인 장비 리스트에서 제거
        _equipmentModel.RemoveEquipmentList(item);
        // 장착중인 장비 리스트에 추가
        _equipmentModel.AddEquippedItemList(item);

        // 저장
        _equipmentModel.SaveData();
    }
    /// <summary>
    /// 장비 해제
    /// </summary>
    /// <param name="type"></param>
    public void UnequipItem(EquipmentType type)
    {
        EquipmentItemData equippedItem = _equipmentModel.GetEquippedItem(type);

        // 장착중인 장비가 없을 시 리턴
        if (equippedItem == null)
            return;

        // 장비 인벤토리에 추가
        _equipmentModel.AddEquipmentList(equippedItem);
        // 장착 중인 리스트에서 제거
        _equipmentModel.RemoveEquippedItemList(equippedItem);
        // 저장
        _equipmentModel.SaveData();
    }
    /// <summary>
    /// 장비 아이템 최대 레벨 구하는 함수
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public int GetEquipmentItemMaxLevel(EquipmentItemData data)
    {
        int value = data.Rarity switch
        {
            ItemRarity.Common => 10,
            ItemRarity.UnCommon => 20,
            ItemRarity.Rare => 30,
            ItemRarity.Unique => 40,
            ItemRarity.Epic => 70,
            ItemRarity.Legendary => 110,
            ItemRarity.Special => 200,
            _ => 10
        };

        value += data.RarityLevel * 10;

        return value;
    }

    /// <summary>
    /// 아이템 레벨업에 필요한 골드 함수
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetUpgradeGoldCost(int level)
    {
        if (_upgradeTableMap.TryGetValue(level, out LevelUpCost cost))
            return cost.GoldCost;
        return 0;
    }
    /// <summary>
    /// 아이템 레벨업에 필요한 스크롤 함수
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetUpgradeScrollCost(int level)
    {
        if(_upgradeTableMap.TryGetValue(level, out LevelUpCost cost))
            return cost.ScrollCost;
        return 0;
    }
    /// <summary>
    /// 아이템 레벨업 함수
    /// </summary>
    /// <param name="data"></param>
    public bool ItemLevelUp(EquipmentItemData data)
    {
        // 필요한 골드
        int goldCost = GetUpgradeGoldCost(data.ItemLevel);
        // 필요한 스크롤
        int scrollCost = GetUpgradeScrollCost(data.ItemLevel);

        // 필요한 골드 코스트와 스크롤 코스트가 없을 시
        if (goldCost <= 0 && scrollCost <= 0)
            return false;

        // 골드나 스크롤이 부족할 시
        if (_gameModel.Gold < goldCost || _gameModel.UpgradeScroll[(int)data.EquipmentType] < scrollCost)
            return false;

        // 골드 차감
        _gameModel.AddGold(-goldCost);
        // 스크롤 차감
        _gameModel.AddUpgradeScroll((int)data.EquipmentType, -scrollCost);
        // 아이템 레벨업
        data.ItemLevel++;
        // 저장
        _equipmentModel.SaveData();

        return true;
    }
    /// <summary>
    /// 아이템 최대 레벨업 함수
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public int ItemLevelUpPossible(EquipmentItemData data)
    {
        // 보유한 골드
        int gold = _gameModel.Gold;
        // 보유한 스크롤
        int scroll = _gameModel.UpgradeScroll[(int)data.EquipmentType];

        // 필요한 총 골드
        int totalGoldCost = 0;
        // 필요한 총 스크롤
        int totalScrollCost = 0;
        // 총 레벨 업
        int totalLevelUp = 0;

        // 레벨업 가능한 정도 계산
        while(data.ItemLevel < GetEquipmentItemMaxLevel(data))
        {
            int goldCost = GetUpgradeGoldCost(data.ItemLevel);
            int scrollCost = GetUpgradeScrollCost(data.ItemLevel);

            if (goldCost <= 0 && scrollCost <= 0)
                break;

            if (gold < goldCost || scroll < scrollCost)
                break;

            // 골드 감소
            gold -= goldCost;
            // 스크롤 감소
            scroll -= scrollCost;

            // 총 소모 골드 추가
            totalGoldCost += goldCost;
            // 총 소모 스크롤 추가
            totalScrollCost += scrollCost;

            // 아이템 레벨업
            data.ItemLevel++;
            // 총 레벨업 추가
            totalLevelUp++;
        }
        // 골드 차감
        _gameModel.AddGold(-totalGoldCost);

        // 스크롤 차감
        _gameModel.AddUpgradeScroll((int)data.EquipmentType, -totalScrollCost);

        // 총 상승 레벨 리턴
        return totalLevelUp;
    }
}