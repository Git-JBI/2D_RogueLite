using System.IO;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private DailyAchievementData _dailyAchievementData;
    [SerializeField] private WeeklyAchievementData _weeklyAchievementData;
    [SerializeField] private AchievementData _AchievementData;


    /// <summary>
    /// 일일 업적 데이터 저장
    /// </summary>
    private void SaveDailyData()
    {
        string gameDataToJson = JsonUtility.ToJson(_dailyAchievementData, true);
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, "DailyAchievementData.json");
#else
        string path = Path.Combine(Application.persistentDataPath, "DailyAchievementData.json");
#endif
        File.WriteAllText(path, gameDataToJson);
    }
    /// <summary>
    /// 주간 업적 데이터 저장
    /// </summary>
    private void SaveWeeklyData()
    {
        string gameDataToJson = JsonUtility.ToJson(_weeklyAchievementData, true);
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, "WeeklyAchievementData.json");
#else
        string path = Path.Combine(Application.persistentDataPath, "WeeklyAchievementData.json");
#endif
        File.WriteAllText(path, gameDataToJson);
    }
    /// <summary>
    /// 업적 데이터 저장
    /// </summary>
    private void SaveData()
    {
        string gameDataToJson = JsonUtility.ToJson(_AchievementData, true);
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, "AchievementData.json");
#else
        string path = Path.Combine(Application.persistentDataPath, "AchievementData.json");
#endif
        File.WriteAllText(path, gameDataToJson);
    }
    /// <summary>
    /// 업적 데이터 불러오기
    /// </summary>
    private void LoadData()
    {
        // Daily, Weekly, Total
    }
}