using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_MergeRuleData : ScriptableObject
{
    public List<MergeRule> mergeRules;
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