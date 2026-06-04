using UnityEngine;

public enum AchievementType
{
    Daily,
    Weekley,
}

public enum AchievementRewardType
{
    None,
    Gold,
    Emerald,
    Scroll,
}

[CreateAssetMenu(menuName = "GameDatas/AchievementData", fileName = "AchievementData")]
public class SO_AchievementData : ScriptableObject
{
    public string AchievementId;
    public AchievementType Type;
    public string AchievementName;
    public int TargetValue;
    public AchievementRewardType RewardType;
    public int RewardValue;
}
