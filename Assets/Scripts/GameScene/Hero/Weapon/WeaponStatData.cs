using UnityEngine;

public enum WeaponStatType
{
    DamageMagnification,        // 데미지 배율
    BulletSpeed,                // 총알 속도
    BulletCount,                // 한 번 쿨타임에 발사하는 총알 갯수
    Cooltime,                   // 공격 쿨타임
    FireDelay,                  // 총알 한발 사이의 딜레이
    SpreadAngle,                // 탄 퍼짐 범위
    RotSpeed,                   // 회전 속도
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
