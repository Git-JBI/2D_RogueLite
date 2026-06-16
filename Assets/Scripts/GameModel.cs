using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameModel : MonoBehaviour
{
    /// <summary>
    /// 저장되어야 할 게임데이터
    /// </summary>
    [SerializeField] private GameData _gameData;

    private HeroData _heroData;

    private int _equipmentBonusAtk;
    private int _equipmentBonusHp;

    public HeroData HeroData
    {
        get { return _heroData; }
        set
        {
            _heroData = value;
            _gameData.HeroDataName = value.HeroName;
        }
    }

    public int Gold => _gameData.Gold;
    public int Emerald => _gameData.Emerald;
    public int[] UpgradeLevel => _gameData.UpgradeLevel;
    public int MaxStage => _gameData.MaxStage;
    public int SelectStage => _gameData.SelectStage;
    public int[] UpgradeScroll => _gameData.UpgradeScroll;
    public string LastDailyResetDate => _gameData.LastDailyResetDate;
    public string LastWeeklyResetDate => _gameData.LastWeeklyResetDate;

    public List<AchievementProgressData> DailyAchievements => _gameData.DailyAchievements;
    public List<AchievementProgressData> WeeklyAchievements => _gameData.WeeklyAchievements;
    public List<AchievementProgressData> TotalAchievements => _gameData.TotalAchievements;
    public List<EquipmentItemData> InventoryEquipmentItems => _gameData.InventoryEquipmentItems;
    public List<EquipmentItemData> EquippedItems => _gameData.EquippedItems;

    private void Awake()
    {
        LoadData();

        InitModel();
    }

    /// <summary>
    /// 골드 추가 함수
    /// </summary>
    /// <param name="value"></param>
    public void AddGold(int value)
    {
        _gameData.Gold += value;
        SaveData();
    }
    /// <summary>
    /// 에메랄드 추가 함수
    /// </summary>
    /// <param name="value"></param>
    public void AddEmerald(int value)
    {
        _gameData.Emerald += value;
        SaveData();
    }
    /// <summary>
    /// 업그레이드 스크롤 추가 함수
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public void AddUpgradeScroll(int type, int value)
    {
        _gameData.UpgradeScroll[type] += value;
        SaveData();
    }
    /// <summary>
    /// 랜덤으로 정해진 숫자대로 스크롤을 추가하는 함수
    /// </summary>
    /// <param name="value"></param>
    public void AddRandomUpgradeScroll(int value)
    {
        System.Random random = new();
        List<int> bars = new();

        for(int i = 0; i < 5; i++)
        {
            bars.Add(random.Next(0, value));
        }
        bars.Sort();

        // 0번 스크롤 추가
        AddUpgradeScroll(0, bars[0]);

        // 1~4번 스크롤 추가
        for(int i = 1; i < 5; i++)
        {
            int scrollValue = bars[i] - bars[i - 1];
            AddUpgradeScroll(i, scrollValue);
        }

        // 5번 스크롤 추가
        int lastScrollValue = value - bars[bars.Count - 1];
        AddUpgradeScroll(5, lastScrollValue);
    }
    /// <summary>
    /// 최대 스테이지 설정 함수
    /// </summary>
    /// <param name="value"></param>
    public void SetMaxStage(int value)
    {
        _gameData.MaxStage = value;
        SaveData();
    }
    /// <summary>
    /// 선택한 스테이지 설정 함수
    /// </summary>
    /// <param name="value"></param>
    public void SetSelectStage(int value)
    {
        _gameData.SelectStage = value;
        SaveData();
    }
    /// <summary>
    /// 최근 일일접속체크용 기록 설정 함수
    /// </summary>
    /// <param name="today"></param>
    public void SetLastDailyResetDate(string today)
    {
        _gameData.LastDailyResetDate = today;
        SaveData();
    }
    /// <summary>
    /// 최근 주간접속체크용 기록 설정 함수
    /// </summary>
    /// <param name="week"></param>
    public void SetLastWeeklyResetDate(string week)
    {
        _gameData.LastWeeklyResetDate = week;
        SaveData();
    }
    /// <summary>
    /// 영웅 선택
    /// </summary>
    /// <param name="value"></param>
    public void SelectHero(HeroData value)
    {
        _heroData = value;
        _gameData.HeroDataName = _heroData.HeroName;
        SaveData();
    }
    /// <summary>
    /// 업그레이드 레벨 증가
    /// </summary>
    /// <param name="value">업그레이드 레벨 종류</param>
    public void IncreaseUpgradeLevel(int value)
    {
        _gameData.UpgradeLevel[value]++;
        SaveData();
    }
    /// <summary>
    /// 최대 스테이지 +1 증가 함수
    /// </summary>
    public void IncreaseMaxStage()
    {
        _gameData.MaxStage++;
        SaveData();
    }
    /// <summary>
    /// 장비 아이템 획득
    /// </summary>
    /// <param name="value">장비 아이템 데이터</param>
    public void GetEquipItem(EquipmentItemData data)
    {
        _gameData.InventoryEquipmentItems.Add(data);
        SaveData();
    }
    /// <summary>
    /// 장비 아이템 장착
    /// </summary>
    /// <param name="instanceId"></param>
    public void EquipItem(string instanceId)
    {
        // 새로 장착할 아이템
        EquipmentItemData item = _gameData.InventoryEquipmentItems.Find(x => x.InstanceID == instanceId);

        // 아이템이 없을 시 리턴
        if (item == null)
            return;

        // 장착 중인 아이템
        EquipmentItemData equippedItem = _gameData.EquippedItems.Find(x => x.EquipmentType == item.EquipmentType);

        // 장착중인 아이템이 있을 시
        if(equippedItem != null)
        {
            // 장비 아이템 벗은 후 인벤토리 추가
            _gameData.EquippedItems.Remove(equippedItem);
            _gameData.InventoryEquipmentItems.Add(equippedItem);
        }

        // 아이템 장착
        _gameData.InventoryEquipmentItems.Remove(item);
        _gameData.EquippedItems.Add(item);

        // 저장
        SaveData();
    }
    /// <summary>
    /// 장비 아이템 탈착
    /// </summary>
    /// <param name="type"></param>
    public void UnequipItem(EquipmentType type)
    {
        EquipmentItemData equippedItem = _gameData.EquippedItems.Find(x => x.EquipmentType == type);

        // 장착중인 장비가 없을 시 리턴
        if(equippedItem == null)
            return;

        _gameData.EquippedItems.Remove(equippedItem);
        _gameData.InventoryEquipmentItems.Add(equippedItem);

        SaveData();
    }
    /// <summary>
    /// 장비 아이템 레벨 업
    /// </summary>
    /// <param name="goldCost">쓰인 골드</param>
    /// <param name="scrollType">쓰인 스크롤 타입</param>
    /// <param name="scrollCost">쓰인 스크롤 갯수</param>
    public void UpgradeEquipmentItemLevel(int goldCost, int scrollType, int scrollCost)
    {
        // 골드 소모
        _gameData.Gold -= goldCost;
        // 스크롤 소모
        _gameData.UpgradeScroll[scrollType] -= scrollCost;
        // 저장
        SaveData();
    }
    /// <summary>
    /// 전역용도 게임 데이터 저장
    /// </summary>
    public void SaveGameData()
    {
        SaveData();
    }
    /// <summary>
    /// 게임 데이터 저장
    /// </summary>
    private void SaveData()
    {
        string gameDataToJson = JsonUtility.ToJson(_gameData, true);
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, "gameData.json");
#else
        string path = Path.Combine(Application.persistentDataPath, "gameData.json");
#endif
        File.WriteAllText(path, gameDataToJson);
    }
    /// <summary>
    /// 게임 데이터 불러오기
    /// </summary>
    private void LoadData()
    {
#if UNITY_EDITOR
        string path = Path.Combine(Application.dataPath, "gameData.json");
#else
        string path = Path.Combine(Application.persistentDataPath, "gameData.json");
#endif
        // 세이브 파일 존재 여부 확인
        if (File.Exists(path))
        {
            string jsonToGameData = File.ReadAllText(path);
            _gameData = JsonUtility.FromJson<GameData>(jsonToGameData);
        }
        // 세이브 파일이 존재하지 않을 경우
        else
        {
            _gameData = new GameData();
            SaveData();
        }
    }
    /// <summary>
    /// 게임 최초 실행 시 없는 데이터들 적용
    /// </summary>
    private void InitModel()
    {
        // 플레이어 캐릭터 설정이 안되어 있을 시
        if (_gameData.HeroDataName == null)
        {
            _heroData = Resources.Load<HeroData>($"GameDatas/HeroData/HeroStatData00");
        }
        else
        {
            _heroData = Resources.Load<HeroData>($"GameDatas/HeroData/HeroStatData{_gameData.HeroDataName}");
        }
        // 최대 스테이지 설정값이 없을 경우
        if(MaxStage == 0)
        {
            SetMaxStage(1);
        }
        // 선택한 스테이지 설정값이 없을 경우
        if(SelectStage == 0)
        {
            SetSelectStage(1);
        }
        // 설정해둔 모든 업적 설정 데이터
        SO_AchievementData[] achievementDatas = Resources.LoadAll<SO_AchievementData>("GameDatas/AchievementData");

        // 각각업적 설정데이터 리스트
        List<SO_AchievementData> dailyDatas = new();
        List<SO_AchievementData> weeklyDatas = new();
        List<SO_AchievementData> totalDatas = new();
        foreach(SO_AchievementData data in achievementDatas)
        {
            switch(data.Type)
            {
                case AchievementType.Daily:
                    dailyDatas.Add(data);
                    break;
                case AchievementType.Weekly:
                    weeklyDatas.Add(data);
                    break;
                case AchievementType.Total:
                    totalDatas.Add(data);
                    break;
            }
        }

        // 일일 업적 데이터길이가 설정데이터 길이와 다를 경우
        if (DailyAchievements.Count != dailyDatas.Count)
        {
            foreach(SO_AchievementData datas in achievementDatas)
            {
                AchievementProgressData data = new();

                if(datas.Type == AchievementType.Daily)
                {
                    data.AchievementId = datas.AchievementId;
                    DailyAchievements.Add(data);
                }
            }
        }
        // 주간 업적 데이터값이 없을 경우
        if (WeeklyAchievements.Count != weeklyDatas.Count)
        {
            foreach (SO_AchievementData datas in achievementDatas)
            {
                AchievementProgressData data = new();

                if (datas.Type == AchievementType.Weekly)
                {
                    data.AchievementId = datas.AchievementId;
                    WeeklyAchievements.Add(data);
                }
            }
        }
        // 토탈 업적 데이터값이 없을 경우
        if (TotalAchievements.Count != totalDatas.Count)
        {
            foreach (SO_AchievementData datas in achievementDatas)
            {
                AchievementProgressData data = new();

                if (datas.Type == AchievementType.Total)
                {
                    data.AchievementId = datas.AchievementId;
                    TotalAchievements.Add(data);
                }
            }
        }
    }
}