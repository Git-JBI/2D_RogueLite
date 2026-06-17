using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private AchievementModel _achievementModel;

    private List<SO_AchievementData> _achievementDatas = new();

    public IReadOnlyList<SO_AchievementData> AchievementDatas => _achievementDatas;

    private void Awake()
    {
        _achievementModel = GameManager.Instance.GameModel.AchievementModel;
        _achievementDatas = Resources.LoadAll<SO_AchievementData>("GameDatas/AchievementData").ToList();
    }

    private void Start()
    {
        CheckDailyDate();
        CheckWeeklyDate();

        // 로그인 했는지 확인
        CheckLogin();
    }

    /// <summary>
    /// 설정 데이터 불러오기
    /// </summary>
    /// <param name="achievementId">업적 ID</param>
    /// <returns></returns>
    public SO_AchievementData GetAchievementData(string achievementId)
    {
        return _achievementDatas.Find(x => x.AchievementId == achievementId);
    }
    /// <summary>
    /// 업적 내용 갱신
    /// </summary>
    /// <param name="conditionType"></param>
    /// <param name="amount"></param>
    public void Report(AchievementConditionType conditionType, int amount = 1)
    {
        foreach (var achievement in _achievementDatas)
        {
            if (achievement.ConditionType != conditionType)
                continue;

            AchievementProgressData progress = GetProgress(achievement);

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
    /// 업적 데이터 가져오기
    /// </summary>
    /// <param name="achievement"></param>
    /// <returns></returns>
    private AchievementProgressData GetProgress(SO_AchievementData achievement)
    {
        // 업적 Type에 따른 리스트를 가져온다(Daily, Weekly, Total)
        List<AchievementProgressData> progressList = GetProgressList(achievement.Type);

        // AchievementId에 맞는 업적을 가져온다
        AchievementProgressData progress = progressList.Find(
            x => x.AchievementId == achievement.AchievementId
        );

        // 해당 업적이 존재하지 않는다면
        if (progress == null)
        {
            // 업적 생성
            progress = new AchievementProgressData
            {
                AchievementId = achievement.AchievementId,
                Progress = 0,
                IsCompleted = false,
                IsRewardClaimed = false
            };

            // 업적 추가
            progressList.Add(progress);
        }

        return progress;
    }
    //TODO 변경 필수 빨리
    /// <summary>
    /// 업적 리스트 가져오기
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public List<AchievementProgressData> GetProgressList(AchievementType type)
    {
        List<AchievementProgressData> list = new();
        return list;
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
        // 일일업적 리스트 가져오기
        List<AchievementProgressData> dailyAchievementList = GetProgressList(AchievementType.Daily);

        // 일일업적 리스트 순회
        foreach (AchievementProgressData data in dailyAchievementList)
        {
            if(data.AchievementId == "Daily_LogIn")
            {
                // 금일 로그인을 하지 않았다면
                if(data.Progress == 0)
                {
                    Report(AchievementConditionType.Login, 1);
                }
            }
        }
    }
}