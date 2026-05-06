using UnityEngine;

public class GameData
{
    private HeroData _heroData;
    private int _gold;

    public HeroData HeroData
    {
        get { return _heroData; }
        set { _heroData = value; }
    }
    public int Gold => _gold;

    public void ChangeGold(int value)
    {
        _gold += value;
    }
}
