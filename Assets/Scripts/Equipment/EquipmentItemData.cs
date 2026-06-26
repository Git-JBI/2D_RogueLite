using UnityEngine;

[System.Serializable]
public class EquipmentItemData
{
    [SerializeField] private string _instanceID;
    [SerializeField] private string _itemID;
    [SerializeField] private EquipmentType _equipmentType;
    [SerializeField] private ItemRarity _rarity;
    [SerializeField] private int _rarityLevel;
    [SerializeField] private int _itemLevel;

    public EquipmentItemData(string instanceID, string itemID, EquipmentType type, ItemRarity rarity, int rarityLevel, int itemLevel)
    {
        _instanceID = instanceID;
        _itemID = itemID;
        _equipmentType = type;
        _rarity = rarity;
        _rarityLevel = rarityLevel;
        _itemLevel = itemLevel;
    }

    public string InstanceID
    {
        get { return _instanceID; }
        set { _instanceID = value;}
    }
    public string ItemID
    {
        get { return _itemID; }
        set { _itemID = value; }
    }
    public EquipmentType EquipmentType
    {
        get{ return _equipmentType; }
        set { _equipmentType = value; }
    }
    public ItemRarity Rarity
    {
        get{ return _rarity; }
        set { _rarity = value; }
    }
    public int RarityLevel
    {
        get { return _rarityLevel; }
        set { _rarityLevel = value; }
    }
    public int ItemLevel
    {
        get { return _itemLevel; }
        set { _itemLevel = value; }
    }
}