[System.Serializable]
public class DailyAchievementData
{
    SO_AchievementData _data;
    public string AchievementId;
    /// <summary>
    /// 오늘 최초 로그인 판별
    /// </summary>
    public bool IsFirstLogin;

    public int Count;

    public bool HasCleared => Count >= _data.TargetValue;

    public void AddCount(int count)
    {
        if (HasCleared == true) return;

        Count += count;
    }
}

/*  적 처치 수
 *  보스 처치 수
 *  
 *  
 *  
 */