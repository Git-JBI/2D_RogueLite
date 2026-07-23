using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/WeaponData", fileName = "WeaponData")]
public class SO_WeaponData : ScriptableObject
{
    [SerializeField] private WeaponType _weaponName;
    [SerializeField] private Sprite _weaponIcon;
    [SerializeField] private WeaponStatData[] _statDatas;
    [SerializeField] private string[] _desces;

    public WeaponType WeaponName => _weaponName;
    public Sprite WeaponIcon => _weaponIcon;
    public string[] Descs => _desces;
    public int MaxLevel => _desces.Length;

    Dictionary<WeaponStatType, WeaponStatData> _statDataMap = new();

    private void OnEnable()
    {
        _statDataMap.Clear();

        foreach(var statData in _statDatas)
        {
            _statDataMap[statData.StatType] = statData;
        }
    }

    public float GetStatValue(WeaponStatType type, int level)
    {
        if (_statDataMap.ContainsKey(type))
            return _statDataMap[type].GetValue(level - 1);
        else
        {
            Debug.Log($"{type}종류 스텟은 {_weaponName} 무기 데이터에 존재하지 않는다.");
            return 0;
        }
    }
}
