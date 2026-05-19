using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class GameModel : MonoBehaviour
{
    [SerializeField] private GameData _gameData = new GameData();
    private HeroData _heroData;

    public event UnityAction<int> OnAddGold;

    public HeroData HeroData
    {
        get { return _heroData; }
        set
        {
            _heroData = value;
            _gameData.heroDataName = value.name;
        }
    }

    public int Gold
    {
        get { return _gameData.gold; }
    }

    public int[] UpgradeLevel
    {
        get { return _gameData.upgradeLevel; }
        set { _gameData.upgradeLevel = value; }
    }

    private void Awake()
    {
        LoadData();

        InitModel();
    }

    public void AddGold(int value)
    {
        _gameData.gold += value;
        SaveData();
    }

    /// <summary>
    /// 영웅 선택
    /// </summary>
    /// <param name="value"></param>
    public void SelectHero(HeroData value)
    {
        _heroData = value;
        _gameData.heroDataName = _heroData.HeroName;
        SaveData();
    }

    /// <summary>
    /// 업그레이드 레벨 증가
    /// </summary>
    /// <param name="value">업그레이드 레벨 종류</param>
    public void IncreaseUpgradeLevel(int value)
    {
        _gameData.upgradeLevel[value]++;
        SaveData();
    }

    /// <summary>
    /// 게임 데이터 저장
    /// </summary>
    public void SaveData()
    {
        string gameDataToJson = JsonUtility.ToJson(_gameData, true);
        string path = Path.Combine(Application.dataPath, "gameData.json");
        File.WriteAllText(path, gameDataToJson);
    }

    /// <summary>
    /// 게임 데이터 불러오기
    /// </summary>
    private void LoadData()
    {
        string path = Path.Combine(Application.dataPath, "gameData.json");
        string jsonToGameData = File.ReadAllText(path);
        _gameData = JsonUtility.FromJson<GameData>(jsonToGameData);
    }

    /// <summary>
    /// 게임 최초 실행 시 없는 데이터들 적용
    /// </summary>
    private void InitModel()
    {
        if (_gameData.heroDataName == null)
        {
            _heroData = Resources.Load<HeroData>($"GameDatas/HeroData/HeroStatData00");
        }
        else
        {
            _heroData = Resources.Load<HeroData>($"GameDatas/HeroData/HeroStatData{_gameData.heroDataName}");
        }
    }
}
