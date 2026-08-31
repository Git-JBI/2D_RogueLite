using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementModel
{
    private GameData _gameData;
    private GameModel _gameModel;

    public bool IsTodayLogin
    {
        get
        {
            AchievementProgressData data = GetAchievementData("Daily_Login");

            return data != null && data.Progress > 0;
        }
    }

    public string LastDailyResetDate => _gameData.LastDailyResetDate;
    public string LastWeeklyResetDate => _gameData.LastWeeklyResetDate;

    public AchievementModel(GameData gameData, GameModel gameModel)
    {
        _gameData = gameData;
        _gameModel = gameModel;
    }

    public IReadOnlyList<AchievementProgressData> Achievements => _gameData.Achievements;

    /// <summary>
    /// 업적 관련 초기화
    /// </summary>
    public void Init()
    {
        // 내용변경 여부 확인용 bool
        bool isChanged = false;

        // 일일 설정 날짜가 없는 경우
        if (string.IsNullOrEmpty(_gameData.LastDailyResetDate))
        {
            SetLastDailyResetDate(DateTime.Now.ToString("yyyy-MM-dd"));
            isChanged = true;
        }

        // 주간 설정 날짜가 없는 경우
        if (string.IsNullOrEmpty(_gameData.LastWeeklyResetDate))
        {
            int diff = ((int)DateTime.Now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime monday = DateTime.Now.Date.AddDays(-diff);
            SetLastWeeklyResetDate(monday.ToString("yyyy-MM-dd"));
            isChanged = true;
        }

        // 모든 업적 설정 데이터 리스트
        List<SO_AchievementData> achievementDatas = Resources.LoadAll<SO_AchievementData>("GameDatas/AchievementData").ToList();

        
        // 모든 업적 설정 데이터 리스트 순회
        foreach(SO_AchievementData configData in achievementDatas)
        {
            // 설정데이터 이상 여부 확인
            if(configData == null || string.IsNullOrEmpty(configData.AchievementId))
            {
                Debug.Log("설정 데이터 이상");
                continue;
            }

            // 설정 데이터의 AchievementId에 따른 _gameData.Achievements내의 업적 찾기
            AchievementProgressData data = _gameData.Achievements.Find(x => x != null && x.AchievementId == configData.AchievementId);

            if (data != null)
                continue;

            _gameData.Achievements.Add(new AchievementProgressData
            {
                AchievementId = configData.AchievementId,
                Progress = 0,
                IsCompleted = false,
                IsRewardClaimed = false
            });

            isChanged = true;
        }

        // 바뀐 내용이 있다면 저장
        if (isChanged)
            SaveData();
    }
    /// <summary>
    /// 일일접속체크용 기록 설정 함수
    /// </summary>
    /// <param name="today"></param>
    public void SetLastDailyResetDate(string today)
    {
        _gameData.LastDailyResetDate = today;
    }

    /// <summary>
    /// 주간접속체크용 기록 설정 함수
    /// </summary>
    /// <param name="week"></param>
    public void SetLastWeeklyResetDate(string week)
    {
        _gameData.LastWeeklyResetDate = week;
    }

    /// <summary>
    /// 업적 ID를 통해 게임데이터에서 해당 업적데이터를 가져오는 함수
    /// </summary>
    /// <param name="achievementId"></param>
    /// <returns></returns>
    public AchievementProgressData GetAchievementData(string achievementId)
    {
        AchievementProgressData data = _gameData.Achievements.Find(x => x.AchievementId == achievementId);
        return data;
    }

    /// <summary>
    /// 일일 업적 초기화
    /// </summary>
    public void ResetDailyAchievements()
    {
        // 업적 설정 데이터들
        List<SO_AchievementData> dailyDatas = Resources.LoadAll<SO_AchievementData>("GameDatas/AchievementData").Where(x => x.Type == AchievementType.Daily).ToList();

        // 일일업적에 해당되는 업적 리스트 내용 초기화
        foreach(SO_AchievementData configData in dailyDatas)
        {
            AchievementProgressData data = GetAchievementData(configData.AchievementId);
            data.Progress = 0;
            data.IsCompleted = false;
            data.IsRewardClaimed = false;
        }
    }
    /// <summary>
    /// 주간 업적 초기화
    /// </summary>
    public void ResetWeeklyAchievements()
    {
        // 주간 업적 설정 데이터들
        List<SO_AchievementData> weeklyDatas = Resources.LoadAll<SO_AchievementData>("GameDatas/AchievementData").Where(x => x.Type == AchievementType.Weekly).ToList();

        // 주간업적에 해당되는 업적 리스트 내용 초기화
        foreach (SO_AchievementData configData in weeklyDatas)
        {
            AchievementProgressData data = _gameData.Achievements.Find(x => x.AchievementId == configData.AchievementId);
            data.Progress = 0;
            data.IsCompleted = false;
            data.IsRewardClaimed = false;
        }
    }


    /// <summary>
    /// 저장
    /// </summary>
    public void SaveData()
    {
        _gameModel.SaveGameData();
    }
}