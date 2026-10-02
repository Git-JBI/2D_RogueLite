public class AppConfigModel
{
    private GameData _gameData;
    private GameModel _gameModel;

    public AppConfigModel(GameData data, GameModel model)
    {
        _gameData = data;
        _gameModel = model;
    }

    public bool GetBgmEnable()
    {
        return _gameData.AppConfigData.IsBgmEnabled;
    }
    public bool GetSfxEnable()
    {
        return _gameData.AppConfigData.IsSfxEnabled;
    }
    public bool GetDamageFloaterEnable()
    {
        return _gameData.AppConfigData.IsDamageFloaterEnabled;
    }
    public void SetBgmEnable(bool value)
    {
        _gameData.AppConfigData.IsBgmEnabled = value;
        _gameModel.SaveGameData();
    }
    public void SetSfxEnable(bool value)
    {
        _gameData.AppConfigData.IsSfxEnabled = value;
        _gameModel.SaveGameData();
    }
    public void SetDamageFloaterEnable(bool value)
    {
        _gameData.AppConfigData.IsDamageFloaterEnabled = value;
        _gameModel.SaveGameData();
    }
}
