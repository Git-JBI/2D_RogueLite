using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameModel : MonoBehaviour
{
    /// <summary>
    /// 게임 내에서 사용할 게임데이터
    /// </summary>
    [SerializeField] private GameData _gameData;
    private EquipmentModel _equipmentModel;
    private AchievementModel _achievementModel;

    private HeroData _heroData;

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

    public EquipmentModel EquipmentModel => _equipmentModel;
    public AchievementModel AchievementModel => _achievementModel;

    private void Awake()
    {
        // 저장된 게임 데이터 불러오기
        LoadData();

        _equipmentModel = new EquipmentModel(_gameData, this);
        _achievementModel = new AchievementModel(_gameData, this);

        InitModel();
    }

    /// <summary>
    /// 골드 추가 함수
    /// </summary>
    /// <param name="value"></param>
    public void AddGold(int value)
    {
        _gameData.Gold += value;
    }
    /// <summary>
    /// 에메랄드 추가 함수
    /// </summary>
    /// <param name="value"></param>
    public void AddEmerald(int value)
    {
        _gameData.Emerald += value;
    }
    /// <summary>
    /// 업그레이드 스크롤 추가 함수
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public void AddUpgradeScroll(int type, int value)
    {
        _gameData.UpgradeScroll[type] += value;
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
    }
    /// <summary>
    /// 선택한 스테이지 설정 함수
    /// </summary>
    /// <param name="value"></param>
    public void SetSelectStage(int value)
    {
        _gameData.SelectStage = value;
    }
    /// <summary>
    /// 영웅 선택
    /// </summary>
    /// <param name="value"></param>
    public void SelectHero(HeroData value)
    {
        _heroData = value;
        _gameData.HeroDataName = _heroData.HeroName;
    }
    /// <summary>
    /// 업그레이드 레벨 증가
    /// </summary>
    /// <param name="value">업그레이드 레벨 종류</param>
    public void IncreaseUpgradeLevel(int value)
    {
        _gameData.UpgradeLevel[value]++;
    }
    /// <summary>
    /// 최대 스테이지 +1 증가 함수
    /// </summary>
    public void IncreaseMaxStage()
    {
        _gameData.MaxStage++;
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
        _achievementModel.Init();
    }
}