using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/AwakeningWeaponConfigData", fileName = "AwakeningWeapon")]
public class SO_AwakeningWeaponConfigData : ScriptableObject
{
    [SerializeField] private AwakeningWeaponType _weaponType;
    [SerializeField] private WeaponStatData[] _statDatas;

    public AwakeningWeaponType WeaponType => _weaponType;
    public WeaponStatData[] StatDatas => _statDatas;

    Dictionary<WeaponStatType, WeaponStatData> _statDataMap = new();

    private void Awake()
    {
        foreach(WeaponStatData data in _statDatas)
        {
            _statDataMap.Add(data.StatType, data);
        }
    }

    public float GetStatValue(WeaponStatType type)
    {
        if (_statDataMap.ContainsKey(type))
            return _statDataMap[type].GetValue(0);
        else
        {
            Debug.Log($"{type}종류 스텟은 {_weaponType} 무기 데이터에 존재하지 않는다.");
            return 0;
        }
    }
}