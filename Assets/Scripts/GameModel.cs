using System.IO;
using UnityEngine;

public class GameModel : MonoBehaviour
{
    private GameData _gameData = new GameData();
    private HeroData _heroData;

    public HeroData HeroData
    {
        get { return _heroData; }
        set
        {
            _heroData = value;
            _gameData.heroDataName = value.name;
        }
    }

    //TODO 이런식으로 하는게 맞는지 _gameData의 골드는 그냥 가져다써야하나
    private void Awake()
    {
        LoadData();
        _heroData = Resources.Load<HeroData>($"GameDatas/HeroData/{_gameData.heroDataName}.asset");
    }

    public void SaveData()
    {
        string gameDataToJson = JsonUtility.ToJson(_gameData, true);
        string path = Path.Combine(Application.dataPath, "gameData.json");
        File.WriteAllText(path, gameDataToJson);
    }

    private void LoadData()
    {
        string path = Path.Combine(Application.dataPath, "gameData.json");
        string jsonToGameData = File.ReadAllText(path);
        _gameData = JsonUtility.FromJson<GameData>(jsonToGameData);
    }
}
