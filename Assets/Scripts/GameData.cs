using UnityEngine;

public class GameData
{
    private string _selectHeroName;
    private int _gold;

    public string SelectHeroName
    {
        get { return _selectHeroName; }
        set { _selectHeroName = value; }
    }
    public int Gold => _gold;

    public void ChangeGold(int value)
    {
        _gold += value;
    }
}
