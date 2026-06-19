using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private AchievementModel _achievementModel;

    private List<SO_AchievementData> _achievementConfigDatas = new();

    public IReadOnlyList<SO_AchievementData> AchievementDatas => _achievementConfigDatas;

    private void Awake()
    {
        _achievementModel = GameManager.Instance.GameModel.AchievementModel;
        _achievementConfigDatas = Resources.LoadAll<SO_AchievementData>("GameDatas/AchievementData").ToList();
    }

    private void Start()
    {
        CheckDailyDate();
        CheckWeeklyDate();

        // 금일 로그인 했는지 확인
        CheckLogin();
    }

    /// <summary>
    /// 업적 설정 데이터 불러오기
    /// </summary>
    /// <param name="achievementId">업적 ID</param>
    /// <returns></returns>
    public SO_AchievementData GetAchievementData(string achievementId)
    {
        return _achievementConfigDatas.Find(x => x.AchievementId == achievementId);
    }
    /// <summary>
    /// 업적 내용 갱신
    /// </summary>
    /// <param name="conditionType"></param>
    /// <param name="amount"></param>
    public void Report(AchievementConditionType conditionType, int amount = 1)
    {
        foreach (var achievement in _achievementConfigDatas)
        {
            if (achievement.ConditionType != conditionType)
                continue;

            AchievementProgressData progress = _achievementModel.GetAchievementData(achievement.AchievementId);

            if (progress.IsRewardClaimed)
                continue;

            progress.Progress += amount;

            if (progress.Progress >= achievement.TargetValue)
            {
                progress.Progress = achievement.TargetValue;
                progress.IsCompleted = true;
            }
        }

        _achievementModel.SaveData();
    }

    /// <summary>
    /// 업적 보상 받는 함수
    /// </summary>
    /// <param name="achievementId"></param>
    public void GetAchievementReward(AchievementProgressData progressData)
    {
        // progressData가 없을 경우
        if (progressData == null)
            return;
        // 업적을 완료하지 않았거나 보상을 받았을 경우
        if (!progressData.IsCompleted || progressData.IsRewardClaimed)
            return;
        SO_AchievementData data = GetAchievementData(progressData.AchievementId);

        // progressData의 id에 해당하는 SO_AchievementData가 없을 경우
        if (data == null)
            return;

        switch(data.RewardType)
        {
            case AchievementRewardType.Gold:
                GameManager.Instance.GameModel.AddGold(data.RewardValue);
                break;
            case AchievementRewardType.Emerald:
                GameManager.Instance.GameModel.AddEmerald(data.RewardValue);
                break;
            case AchievementRewardType.Scroll:
                GameManager.Instance.GameModel.AddRandomUpgradeScroll(data.RewardValue);
                break;
        }

        progressData.IsRewardClaimed = true;
        GameManager.Instance.GameModel.SaveGameData();
    }

    /// <summary>
    /// Daily 체크
    /// </summary>
    private void CheckDailyDate()
    {
        // 오늘 날짜
        string today = DateTime.Now.ToString("yyyy-MM-dd");

        // 오늘 기록된 접속날짜랑 동일한 경우 return
        if (_achievementModel.LastDailyResetDate == today)
            return;

        // 일일 업적 초기화
        _achievementModel.ResetDailyAchievements();

        // 일일 접속 날짜 갱신
        _achievementModel.SetLastDailyResetDate(today);

        // 저장
        _achievementModel.SaveData();
    }
    /// <summary>
    /// 월요일 00시 기준으로 주간 체크
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    private string GetWeekStartDateString(DateTime date)
    {
        int diff = ((int)date.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
        DateTime monday = date.Date.AddDays(-diff);

        return monday.ToString("yyyy-MM-dd");
    }
    /// <summary>
    /// Weekly 체크
    /// </summary>
    private void CheckWeeklyDate()
    {
        string thisWeek = GetWeekStartDateString(DateTime.Now);

        if (_achievementModel.LastWeeklyResetDate == thisWeek)
            return;

        _achievementModel.ResetWeeklyAchievements();

        _achievementModel.SetLastWeeklyResetDate(thisWeek);
        _achievementModel.SaveData();
    }
    /// <summary>
    /// 금일 로그인 체크
    /// </summary>
    private void CheckLogin()
    {
        // 금일 로그인을 하지 않았다면
        if(!_achievementModel.IsTodayLogin)
        {
            Report(AchievementConditionType.Login, 1);
        }
    }
}