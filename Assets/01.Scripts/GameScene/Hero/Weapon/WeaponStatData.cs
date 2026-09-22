using UnityEngine;

public enum WeaponStatType
{
    /// <summary>
    /// 총알 갯수
    /// </summary>
    BulletSpeed,
    /// <summary>
    /// 한 사이클에 발사하는 총알의 갯수
    /// </summary>
    BulletCount,
    /// <summary>
    /// 공격 쿨타임
    /// </summary>
    Cooltime,
    /// <summary>
    /// 총알 사이의 딜레이
    /// </summary>
    FireDelay,
    /// <summary>
    /// 탄 퍼짐 각도
    /// </summary>
    SpreadAngle,
    /// <summary>
    /// 회전 속도
    /// </summary>
    RotSpeed,
    /// <summary>
    /// 지속 시간
    /// </summary>
    Duration,
    /// <summary>
    /// 튕김 횟수
    /// </summary>
    BounceCount,
    /// <summary>
    /// 관통 횟수
    /// </summary>
    PierceCount,
}

[System.Serializable]
public class WeaponStatData
{
    [SerializeField] private WeaponStatType _statType;          // 어떤 종류의 스텟인지
    [SerializeField] private float[] _value;                    // 스텟의 값

    public int MaxLevel => _value.Length - 1;

    public WeaponStatType StatType => _statType;

    public float GetValue(int level)
    {
        if (level < 0)
            return 0;

        if(level >= MaxLevel)
            level = MaxLevel;

        return _value[level];
    }
}
