using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public string HeroDataName;
    public int Gold;
    public int[] UpgradeLevel = { 0, 0, 0 };
    public List<int> EquipItems = new List<int>();
}