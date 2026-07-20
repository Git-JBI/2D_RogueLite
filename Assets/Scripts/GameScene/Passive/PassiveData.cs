using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/PassiveData", fileName = "PassiveData")]
public class PassiveData : ScriptableObject
{
    [SerializeField] private string _passiveName;
    [SerializeField] private Sprite _passiveSprite;
    [SerializeField] private string[] _desces;
    [SerializeField] private PassiveStatData _statData;

    public Sprite PassiveSprite => _passiveSprite;
    public string[] Desces => _desces;

    public float GetStatValue(int level)
    {
        return _statData.GetValue(level);
    }
}
