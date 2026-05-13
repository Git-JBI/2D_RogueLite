using UnityEngine;

[System.Serializable]
public class GameData
{
    private HeroData _heroData;
    [SerializeField] private int _gold;

    public HeroData HeroData
    {
        get { return _heroData; }
        set { _heroData = value; }
    }
    public int Gold => _gold;

    public void AddGold(int value)
    {
        _gold += value;

        // 0 미만으로 내려가지 않음
        if(_gold < 0)
            _gold = 0;
    }
}
