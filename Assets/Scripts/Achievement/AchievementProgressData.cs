using System;

[Serializable]
public class AchievementProgressData
{
    public string AchievementId;
    public int Progress;
    public bool IsCompleted;
    public bool IsRewardClaimed;
}
