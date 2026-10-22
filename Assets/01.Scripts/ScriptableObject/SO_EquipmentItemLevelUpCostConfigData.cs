using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelUpCost
{
    public int Level;
    public int GoldCost;
    public int ScrollCost;
}

[CreateAssetMenu(menuName = "GameDatas/EquipmentItemLevelUpCostConfigData", fileName = "EquipmentItemLevelUpCostConfigData")]
public class SO_EquipmentItemLevelUpCostConfigData : ScriptableObject
{
    [SerializeField] private List<LevelUpCost> _costList;

    public IReadOnlyList<LevelUpCost> CostList => _costList;
}
