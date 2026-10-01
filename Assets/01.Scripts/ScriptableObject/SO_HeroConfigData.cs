using UnityEditor.Animations;
using UnityEngine;

public enum HeroName
{
    None,
    Ninja,
    HolyKnight,
    Skeleton,
}

[CreateAssetMenu(menuName = "GameDatas/HeroConfigData",fileName = "HeroConfigData")]
public class SO_HeroConfigData : ScriptableObject
{
    [SerializeField] private string _heroId;
    [SerializeField] private HeroName _heroName;
    [SerializeField] private AnimatorController _heroUIAnimController;
    [SerializeField] private Sprite _heroPortraitSprite;
    [SerializeField] private HeroStatData _statData;
    [SerializeField] private string _desc;

    public string HeroId => _heroId;
    public HeroName HeroName => _heroName;
    public AnimatorController HeroUIAnimController => _heroUIAnimController;
    public Sprite HeroPortraitSprite => _heroPortraitSprite;
    public HeroStatData StatData => _statData;
    public string Desc => _desc;

    public HeroStatType GetHeroStatType()
    {
        return _statData.StatType;
    }
    public float GetStatValue()
    {
        return _statData.GetValue();
    }
}
