using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class GameModel : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
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

    public int Gold
    {
        get { return _gameData.Gold; }
    }

    public int[] UpgradeLevel
    {
        get { return _gameData.UpgradeLevel; }
        set { _gameData.UpgradeLevel = value; }
    }

    private void Awake()
    {
        LoadData();

        InitModel();
    }

    public void AddGold(int value)
    {
        _gameData.Gold += value;
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
    /// 장비 아이템 획득
    /// </summary>
    /// <param name="value">장비 아이템 키 값</param>
    public void GetEquipItem(EquipmentItemData data)
    {
        _gameData.InventoryItems.Add(data);
        SaveData();
    }

    /// <summary>
    /// 게임 데이터 저장
    /// </summary>
    public void SaveData()
    {
        string gameDataToJson = JsonUtility.ToJson(_gameData, true);
        //TODO 더이상 데이터를 직접 확인할 필요가 없을 때 persistentDataPath로 교체
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
        if (_gameData.HeroDataName == null)
        {
            _heroData = Resources.Load<HeroData>($"GameDatas/HeroData/HeroStatData00");
        }
        else
        {
            _heroData = Resources.Load<HeroData>($"GameDatas/HeroData/HeroStatData{_gameData.HeroDataName}");
        }
    }
}
