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
    Score,
}

[CreateAssetMenu(menuName = "GameDatas/AchievementData", fileName = "AchievementData")]
public class SO_AchievementData : ScriptableObject
{
    [SerializeField] private string _achievementId;
    [SerializeField] private AchievementType _type;
    [SerializeField] private AchievementConditionType _conditionType;

    [SerializeField] private string _achievementName;
    [SerializeField] private int _targetValue;

    [SerializeField] private AchievementRewardType _rewardType;
    [SerializeField] private int _rewardValue;

    public string AchievementId => _achievementId;
    public AchievementType Type => _type;
    public AchievementConditionType ConditionType => _conditionType;

    public string AchievementName => _achievementName;
    public int TargetValue => _targetValue;

    public AchievementRewardType RewardType => _rewardType;
    public int RewardValue => _rewardValue;
}