using UnityEngine;

public enum AchievementType
{
    Daily,
    Weekly,
    Total,
}

public enum AchievementConditionType
{
    Login,                  // 로그인
    KillEnemy,              // 적 처치
    KillBoss,               // 보스 처치
    EquipmentLevelUp,       // 장비 레벨 업
    Gacha,                  // 가챠 횟수
    EnterStage,             // 스테이지 진입
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
    public AchievementConditionType ConditionType;

    public string AchievementName;
    public int TargetValue;

    public AchievementRewardType RewardType;
    public int RewardValue;
}
