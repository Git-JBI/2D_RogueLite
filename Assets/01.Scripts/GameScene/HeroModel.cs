public class HeroModel
{
    private GameData _gameData;
    private GameModel _gameModel;

    private SO_HeroConfigData[] _heroConfigData;

    public HeroModel(GameData data, GameModel model)
    {
        _gameData = data;
        _gameModel = model;
    }

    public void SetHero(string heroId)
    {
        _gameData.HeroId = heroId;
    }

    public string GetHeroName()
    {
        return _heroConfigData[0].HeroName.ToString();
    }
    public HeroStatType GetHeroStatType()
    {
        return _heroConfigData[0].GetHeroStatType();
    }
    public float GetValue()
    {
        return _heroConfigData[0].GetStatValue();
    }

}
