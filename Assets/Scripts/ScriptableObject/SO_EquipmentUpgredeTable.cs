using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelUpCost
{
    public int Level;
    public int GoldCost;
    public int ScrollCost;
}

[CreateAssetMenu(menuName = "GameDatas/EquipmentUpgradeData", fileName = "EquipmentUpgradeData")]
public class SO_EquipmentUpgredeTable : ScriptableObject
{
    public List<LevelUpCost> CostList = new();
}
