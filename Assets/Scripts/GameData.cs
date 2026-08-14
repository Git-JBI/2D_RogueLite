using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public string HeroDataName;
    public int Gold;
    public int Emerald;
    public int Energy;
    public int[] UpgradeLevel = new int[3];
    public int MaxStage;
    public int SelectStage;
    /// <summary>
    /// 0. 무기 1. 목걸이 2. 장갑 3. 갑옷 4. 벨트 5. 신발
    /// </summary>
    public int[] UpgradeScroll = new int[6];

    public int DailyAchievementScore;
    public int WeeklyAchievementScore;

    public string LastLoginTime;
    public string LastDailyResetDate;
    public string LastWeeklyResetDate;

    public List<AchievementProgressData> Achievements = new();

    public List<EquipmentItemData> EquipmentItems = new();
    public List<EquipmentItemData> EquippedItems = new();
    //TODO 일반 아이템 리스트 따로 필요(ex. 뽑기용 열쇠, 장비 강화용 주문서 등등)
}