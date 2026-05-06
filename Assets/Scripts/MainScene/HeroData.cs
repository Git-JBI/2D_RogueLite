using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/HeroStatData",fileName = "HeroStatData")]
public class HeroData : ScriptableObject
{
    [SerializeField] private string _heroName;
    [SerializeField] private Sprite _heroSprite;
    [SerializeField] private HeroStatData _statData;
    [SerializeField] private string _desc;

    public string HeroName => _heroName;
    public Sprite HeroSprite => _heroSprite;
    public string Desc => _desc;

    public float GetStatValue()
    {
        return _statData.GetValue();
    }
}
