using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/PassiveData", fileName = "PassiveData")]
public class PassiveData : ScriptableObject
{
    [SerializeField] private string _passiveName;
    [SerializeField] private Sprite _passiveSprite;
    [SerializeField] private PassiveStatData[] _statDatas;
    [SerializeField] private string[] _desces;

    public Sprite PassiveSprite => _passiveSprite;
    public string[] Desces => _desces;

    public int MaxLevel => _desces.Length;

    Dictionary<PassiveStatType, PassiveStatData> _statDataMap = new();

    private void OnEnable()
    {
        _statDataMap.Clear();

        foreach(var statData in _statDatas)
        {
            _statDataMap[statData.StatType] = statData;
        }
    }

    public float GetStatValue(PassiveStatType type, int level)
    {
        if(_statDataMap.ContainsKey(type))
            return _statDataMap[type].GetValue(level);
        else
        {
            Debug.Log($"{type}종류 스텟은 {_passiveName} 패시브 데이터에 존재하지 않는다.");
            return 0;
        }
    }
}
