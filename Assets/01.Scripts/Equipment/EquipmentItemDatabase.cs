using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentItemDatabase : MonoBehaviour
{
    /// <summary>
    /// 장비 아이템 리스트
    /// </summary>
    private List<SO_EquipmentItemData> _equipmentItems;

    public IReadOnlyList<SO_EquipmentItemData> EquipmentItemDatas => _equipmentItems;

    /// <summary>
    /// 장비 아이템 딕셔너리
    /// </summary>
    private Dictionary<string, SO_EquipmentItemData> _equipmentItemMap = new();

    private void Awake()
    {
        // 장비 아이템데이터 Resources.LoadAll로 전부 불러오기
        _equipmentItems = Resources.LoadAll<SO_EquipmentItemData>("GameDatas/ItemData").ToList();

        // 장비 아이템 딕셔너리에 장비 아이템 데이터 추가
        foreach(SO_EquipmentItemData item in _equipmentItems)
        {
            if (item == null)
                continue;

            if(_equipmentItemMap.ContainsKey(item.Id))
            {
                Debug.Log($"중복된 아이템 {item.Id}가 있습니다.");
                continue;
            }
            _equipmentItemMap.Add(item.Id, item);
        }
    }

    public SO_EquipmentItemData GetEquipmentItemData(string itemId)
    {
        if(_equipmentItemMap.TryGetValue(itemId, out SO_EquipmentItemData itemData))
            return itemData;

        Debug.LogWarning($"{itemId}가 없습니다.");
        return null;
    }

    public string GetEquipmentItemDesc(string itemId)
    {
        if (itemId == "")
            return "";

        SO_EquipmentItemData itemData = GetEquipmentItemData(itemId);

        if (itemData == null)
            return "";

        return itemData.Desc;
    }

    public string GetEquipmentItemName(string itemId)
    {
        SO_EquipmentItemData itemData = GetEquipmentItemData(itemId);

        if (itemData == null)
            return "";

        return itemData.ItemName;
    }
}
