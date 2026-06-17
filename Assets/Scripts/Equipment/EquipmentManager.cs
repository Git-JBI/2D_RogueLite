using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private int _equipmentBonusAtk;
    private int _equipmentBonusHp;

    private EquipmentModel _equipmentModel = GameManager.Instance.GameModel.EquipmentModel;

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

    /// <summary>
    /// 장비 장착
    /// </summary>
    /// <param name="instanceId"></param>
    public void EquipItem(string instanceId)
    {
        // 장비 인벤토리에서 장착할 아이템을 찾기
        EquipmentItemData item = _equipmentModel.FindEquipmentList(instanceId);

        // 장착할 아이템의 타입
        EquipmentType type = item.EquipmentType;

        // 장착 중인 아이템
        EquipmentItemData equippedItem = _equipmentModel.FindEquippedItemList(type);

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
        _equipmentModel.AddEquippedItemList(equippedItem);

        // 저장
        _equipmentModel.SaveData();
    }
    /// <summary>
    /// 장비 해제
    /// </summary>
    /// <param name="type"></param>
    public void UnequipItem(EquipmentType type)
    {
        EquipmentItemData equippedItem = _equipmentModel.FindEquippedItemList(type);

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
        int value = 0;

        // 레어리티에 따른 최대레벨 설정
        switch(data.Rarity)
        {
            case ItemRarity.Common:
                value += 10;
                break;
            case ItemRarity.UnCommon:
                value += 20;
                break;
            case ItemRarity.Rare:
                value += 30;
                break;
            case ItemRarity.Unique:
                value += 40;
                break;
            case ItemRarity.Epic:
                value += 70;
                break;
            case ItemRarity.Legendary:
                value += 110;
                break;
            case ItemRarity.Special:
                value += 200;
                break;
        }

        // 레어리티 레벨당 최대 레벨 10 증가
        value = data.RarityLevel * 10;

        return value;
    }
}