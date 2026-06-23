using UnityEngine;

public enum PassiveStatType
{
    DamagePercent,      // 데미지 증가 퍼센트
    MoveSpeedPercent,   // 이동속도 증가 퍼센트
    MaxHpPercent,       // 최대 체력 증가 퍼센트
}

[System.Serializable]
public class PassiveStatData
{
    [SerializeField] private PassiveStatType _statType;     // 어떤 종류의 스텟인지
    [SerializeField] private float[] _value;                // 스텟의 값

    public int MaxLevel => _value.Length - 1;

    public PassiveStatType StatType => _statType;

    public float GetValue(int level)
    {
        if (level < 0)
            return 0;
        else if (level >= MaxLevel)
            level = MaxLevel;
        return _value[level];
    }
}
