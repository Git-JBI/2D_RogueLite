using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/PassiveData", fileName = "PassiveData")]
public class SO_PassiveData : ScriptableObject
{
    [SerializeField] private PassiveType _passiveType;
    [SerializeField] private Sprite _passiveSprite;
    [SerializeField] private string[] _desces;
    [SerializeField] private PassiveStatData _statData;

    public PassiveType PassiveType => _passiveType;
    public Sprite PassiveSprite => _passiveSprite;
    public string[] Desces => _desces;

    public float GetStatValue(int level)
    {
        return _statData.GetValue(level);
    }
}
