using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public string HeroDataName;
    public int Gold;
    public int Emerald;
    public int[] UpgradeLevel = new int[3];
    public int MaxStage;
    /// <summary>
    /// 0. 무기 1. 목걸이 2. 장갑 3. 갑옷 4. 벨트 5. 신발
    /// </summary>
    public int[] UpgradeScroll = new int[6];

    public int DailyAchievementScore;
    public int WeeklyAchievementScore;

    public string LastDailyResetDate;
    public string LastWeeklyResetDate;

    public List<AchievementProgressData> DailyAchievements = new();
    public List<AchievementProgressData> WeeklyAchievements = new();
    public List<AchievementProgressData> TotalAchievements = new();

    public List<EquipmentItemData> InventoryEquipmentItems = new List<EquipmentItemData>();
    public List<EquipmentItemData> EquippedItems = new List<EquipmentItemData>();
    //TODO 일반 아이템 리스트 따로 필요(ex. 뽑기용 열쇠, 장비 강화용 주문서 등등)
}