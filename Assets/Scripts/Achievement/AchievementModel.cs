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
            return (_gameData.Achievements.Find(x => x.AchievementId == "Daily_LogIn").Progress == 0 ? false : true);
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
        // 설정 날짜가 없는 경우
        if (_gameData.LastDailyResetDate == "")
            SetLastDailyResetDate(DateTime.Now.ToString("yyyy-MM-dd"));
        if(_gameData.LastWeeklyResetDate == "")
        {
            int diff = ((int)DateTime.Now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime monday = DateTime.Now.Date.AddDays(-diff);
            SetLastWeeklyResetDate(monday.ToString("yyyy-MM-dd"));
        }

        // 모든 업적 설정 데이터 리스트
        List<SO_AchievementData> achievementDatas = Resources.LoadAll<SO_AchievementData>("GameDatas/AchievementData").ToList();
        // 설정 데이터 리스트의 길이와 게임 데이터의 업적 리스트의 길이가 다를 경우
        if (achievementDatas.Count != Achievements.Count)
        {
            foreach (SO_AchievementData configdata in achievementDatas)
            {
                // 업적 리스트에 업적 설정 리스트와 맞는 Id가 없는 경우
                if (!Achievements.Any(data => data.AchievementId == configdata.AchievementId))
                {
                    AchievementProgressData newData = new()
                    {
                        AchievementId = configdata.AchievementId,
                        Progress = 0,
                        IsCompleted = false,
                        IsRewardClaimed = false
                    };
                    _gameData.Achievements.Add(newData);
                }
            }
        }
        // 저장
        SaveData();
    }
    /// <summary>
    /// 최근 일일접속체크용 기록 설정 함수
    /// </summary>
    /// <param name="today"></param>
    public void SetLastDailyResetDate(string today)
    {
        _gameData.LastDailyResetDate = today;
    }

    /// <summary>
    /// 최근 주간접속체크용 기록 설정 함수
    /// </summary>
    /// <param name="week"></param>
    public void SetLastWeeklyResetDate(string week)
    {
        _gameData.LastWeeklyResetDate = week;
    }

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
        List<SO_AchievementData> achievementDatas = Resources.LoadAll<SO_AchievementData>("GameDatas/AchievementData").ToList();

        List<SO_AchievementData> dailyDatas = new();

        // 일일업적에 해당하는 설정 데이터 리스트 설정
        foreach(SO_AchievementData configData in achievementDatas)
        {
            if(configData.Type == AchievementType.Daily)
                dailyDatas.Add(configData);
        }

        // 일일업적에 해당되는 업적 리스트 내용 초기화
        foreach(SO_AchievementData configData in dailyDatas)
        {
            AchievementProgressData data = _gameData.Achievements.Find(x => x.AchievementId == configData.AchievementId);
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
        List<SO_AchievementData> achievementDatas = Resources.LoadAll<SO_AchievementData>("GameDatas/AchievementData").ToList();

        List<SO_AchievementData> weeklyDatas = new();

        // 주간업적에 해당하는 설정 데이터 리스트 설정
        foreach (SO_AchievementData configData in achievementDatas)
        {
            if (configData.Type == AchievementType.Weekly)
                weeklyDatas.Add(configData);
        }

        // 일일업적에 해당되는 업적 리스트 내용 초기화
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