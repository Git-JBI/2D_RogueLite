using System.Collections.Generic;
using UnityEngine;

public class EquipmentItemDatabase : MonoBehaviour
{
    [SerializeField] private List<SO_EquipmentItemData> _equipmentItems;

    private Dictionary<string, SO_EquipmentItemData> _equipmentItemMap;

    private void Awake()
    {
        _equipmentItemMap = new Dictionary<string, SO_EquipmentItemData>();

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
        SO_EquipmentItemData itemData = GetEquipmentItemData(itemId);

        if (itemData == null)
            return "";

        return itemData.Desc;
    }
}
