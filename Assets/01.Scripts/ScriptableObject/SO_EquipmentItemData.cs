using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 아이템 등급 (7종류)
/// </summary>
public enum ItemRarity
{
    Common,
    UnCommon,
    Rare,
    Unique,
    Epic,
    Legendary,
    Special,
}

/// <summary>
/// 아이템 티어(3종류)
/// </summary>
public enum ItemTier
{
    Default,
    SClass,
    SSClass,
}

public enum EquipmentStatType
{
    Attack,
    Hp,
}

public enum EquipmentType
{
    Weapon,
    Necklace,
    Gloves,
    Armor,
    Belt,
    Boots,
}

[System.Serializable]
public class RarityStatInfo
{
    [FormerlySerializedAs("Rarity")]
    [SerializeField] private ItemRarity _rarity;
    [FormerlySerializedAs("BaseStat")]
    [SerializeField] private int _baseStat;
    [FormerlySerializedAs("StatPerLevel")]
    [SerializeField] private int _statPerLevel;

    public ItemRarity Rarity => _rarity;
    public int BaseStat => _baseStat;
    public int StatPerLevel => _statPerLevel;
}

[CreateAssetMenu(menuName = "GameDatas/ItemData", fileName = "ItemData")]
public class SO_EquipmentItemData : ScriptableObject
{
    [FormerlySerializedAs("Id")]
    [SerializeField] private string _id;
    [FormerlySerializedAs("ItemName")]
    [SerializeField] private string _itemName;
    [FormerlySerializedAs("EquipmentType")]
    [SerializeField] private EquipmentType _equipmentType;
    [FormerlySerializedAs("EquipmentStatType")]
    [SerializeField] private EquipmentStatType _equipmentStatType;
    [FormerlySerializedAs("ItemTier")]
    [SerializeField] private ItemTier _itemTier;
    [FormerlySerializedAs("IconSprite")]
    [SerializeField] private Sprite _iconSprite;
    [FormerlySerializedAs("Desc")]
    [SerializeField] private string _desc;
    [FormerlySerializedAs("RaritySettings")]
    [SerializeField] private List<RarityStatInfo> _raritySettings;

    public string Id => _id;
    public string ItemName => _itemName;
    public EquipmentType EquipmentType => _equipmentType;
    public EquipmentStatType EquipmentStatType => _equipmentStatType;
    public ItemTier ItemTier => _itemTier;
    public Sprite IconSprite => _iconSprite;
    public string Desc => _desc;
    public IReadOnlyList<RarityStatInfo> RaritySettings => _raritySettings;
}
