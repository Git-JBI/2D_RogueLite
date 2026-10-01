using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class HeroModel
{
    private GameData _gameData;
    private GameModel _gameModel;

    private SO_HeroConfigData[] _heroConfigDatas;

    private Dictionary<string, SO_HeroConfigData> _heroConfigDataMap = new();

    public HeroModel(GameData data, GameModel model)
    {
        _gameData = data;
        _gameModel = model;

        _heroConfigDatas = Resources.LoadAll<SO_HeroConfigData>("GameDatas/HeroConfigData");

        for(int i = 0; i < _heroConfigDatas.Length; i++)
        {
            string id = _heroConfigDatas[i].HeroId;
            _heroConfigDataMap.Add(id, _heroConfigDatas[i]);
        }
    }

    public void SetHero(string heroId)
    {
        _gameData.HeroId = heroId;
        _gameModel.SaveGameData();
    }
    public int GetHeroCount()
    {
        return _heroConfigDatas.Length;
    }
    public string GetHeroId()
    {
        return _gameData.HeroId;
    }
    public HeroName GetHeroName(string heroId)
    {
        if (_heroConfigDataMap.TryGetValue(heroId, out SO_HeroConfigData data))
            return data.HeroName;

        return HeroName.None;
    }
    public string GetHeroNameToString()
    {
        if (_heroConfigDataMap.TryGetValue(_gameData.HeroId, out SO_HeroConfigData data))
            return data.HeroName.ToString();

        return "";
    }
    public AnimatorController GetHeroUIAnimController()
    {
        if (_heroConfigDataMap.TryGetValue(_gameData.HeroId, out SO_HeroConfigData data))
            return data.HeroUIAnimController;

        return null;
    }
    public Sprite GetHeroPortraitSprite(string heroId)
    {
        if (_heroConfigDataMap.TryGetValue(heroId, out SO_HeroConfigData data))
            return data.HeroPortraitSprite;

        return null;
    }
    public HeroStatType GetHeroStatType()
    {
        if (_heroConfigDataMap.TryGetValue(_gameData.HeroId, out SO_HeroConfigData data))
            return data.GetHeroStatType();

        return HeroStatType.None;
    }
    public float GetValue()
    {
        if (_heroConfigDataMap.TryGetValue(_gameData.HeroId, out SO_HeroConfigData data))
            return data.GetStatValue();

        return 0;
    }
    public string GetDesc()
    {
        if (_heroConfigDataMap.TryGetValue(_gameData.HeroId, out SO_HeroConfigData data))
            return data.Desc;

        return "";
    }
}
