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
public class RaritySkillInfo
{
    public ItemRarity Rarity;
    public int MaxLevel;
    public int BaseStat;
    public int StatPerLevel;
}

[CreateAssetMenu(menuName = "GameDatas/ItemData", fileName = "ItemData")]
public class SO_EquipmentItemData : ScriptableObject
{
    public string Id;
    public string ItemName;
    public EquipmentType EquipmentType;
    public EquipmentStatType EquipmentStatType;
    public ItemTier ItemTier;
    public Sprite IconSprite;
    public string Desc;
    public List<RaritySkillInfo> RaritySettings = new();
}
