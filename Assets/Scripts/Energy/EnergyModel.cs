using System;

public class EnergyModel
{
    private GameData _gameData;
    private GameModel _gameModel;

    private int _maxEnergy;

    public int Energy => _gameData.Energy;
    public string LastLoginTime => _gameData.LastLoginTime;

    public EnergyModel(GameData gameData, GameModel model)
    {
        _gameData = gameData;
        _gameModel = model;
        _maxEnergy = 50;

        CheckFirstTime();
    }

    /// <summary>
    /// 에너지를 증가시키는 함수(MaxEnergy인 50을 넘길 수 없도록 설정됨)
    /// </summary>
    /// <param name="value">증가할 에너지 양</param>
    public void IncreaseEnergy(int value = 1)
    {
        _gameData.Energy = Math.Min(_gameData.Energy + value, _maxEnergy);
        _gameModel.SaveGameData();
    }
    /// <summary>
    /// 최근 접속 시간을 갱신하는 함수
    /// </summary>
    /// <param name="time"></param>
    public void SaveLastLoginTime(string time)
    {
        // 마지막 접속시간 설정
        _gameData.LastLoginTime = time;

        // 저장
        _gameModel.SaveGameData();
    }

    /// <summary>
    /// 초기화 시 데이터값 확인용 함수
    /// </summary>
    private void CheckFirstTime()
    {
        // LastLoginTime값이 없을 경우
        if(_gameData.LastLoginTime == "")
        {
            // 현재 에너지 = 최대 에너지 설정
            _gameData.Energy = _maxEnergy;
            // 마지막 접속시간 초기화
            _gameData.LastLoginTime = DateTime.Now.ToBinary().ToString();
            // 저장
            _gameModel.SaveGameData();
        }
    }
}
