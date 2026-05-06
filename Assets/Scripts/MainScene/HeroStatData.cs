using UnityEngine;

public enum HeroStatType
{
    Damage,
    MoveSpeed,
    MaxHp,
}

[System.Serializable]
public class HeroStatData
{
    [SerializeField] private HeroStatType _statType;
    [SerializeField] private float _value;

    public HeroStatType StatType => _statType;

    public float GetValue()
    {
        return _value;
    }
}
