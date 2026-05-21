using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

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

public enum ItemTier
{
    Default,
    SClass,
    SSClass,
}

public enum ItemType
{
    Equipment,
    Consumable,
    Ingredient,
}

public enum EquipmentType
{
    None,
    Weapon,
    Armor,
    Belt,
    Gloves,
    Boots,
    Necklace,
}

[System.Serializable]
public class RaritySkillInfo
{
    public ItemRarity rarity;
    public int maxLevel;
    public int baseStat;
    public int statPerLevel;
}

[CreateAssetMenu(menuName = "GameDatas/ItemData", fileName = "ItemData")]
public class SO_ItemData : ScriptableObject
{
    public string id;
    public string itemName;
    public ItemType itemType;
    public EquipmentType equipmentType;
    public Sprite iconSprite;
    public List<RaritySkillInfo> raritySettings = new();
}
