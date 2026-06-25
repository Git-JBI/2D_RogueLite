using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_MergeRuleData : ScriptableObject
{
    [SerializeField] private List<MergeRule> _mergeRules;

    public MergeRule GetMergeRule(ItemRarity rarity, int rarityLevel)
    {
        foreach(MergeRule x in _mergeRules)
        {
            if (x.BaseitemRarity == rarity && x.BaseitemRarityLevel == rarityLevel)
                return x;
        }
        return null;
    }
}

[System.Serializable]
public class MergeRule
{
    [Header("# 베이스 항목")]
    [SerializeField] private ItemRarity _baseitemRarity;
    [SerializeField] private int _baseitemRarityLevel;

    [Header("# 필요한 항목")]
    [SerializeField] private ItemRarity _requireitemRarity;
    [SerializeField] private int _requireRarityLevel;
    [SerializeField] private int _requireCount;

    public ItemRarity BaseitemRarity => _baseitemRarity;
    public int BaseitemRarityLevel => _baseitemRarityLevel;
    public ItemRarity RequireitemRarity => _requireitemRarity;
    public int RequireRarityLevel => _requireRarityLevel;
    public int RequireCount => _requireCount;
}