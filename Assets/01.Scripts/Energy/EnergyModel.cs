using System;

public class EnergyModel
{
    private GameData _gameData;
    private GameModel _gameModel;

    private int _maxEnergy;

    public int Energy => _gameData.Energy;
    public int MaxEnergy => _maxEnergy;
    public int EnergyRechargeRemainingTime => _gameData.EnergyRechargeRemainingTime;
    public string LastLoginTime => _gameData.LastLoginTime;
    public int RemainChargeEnergy => _maxEnergy - _gameData.Energy;
    public bool IsEnergyFull => _gameData.Energy == _maxEnergy ? true : false;

    public EnergyModel(GameData gameData, GameModel model)
    {
        _gameData = gameData;
        _gameModel = model;
        //TODO 최대 에너지의 변화가 있을 경우 없어져야할 부분
        _maxEnergy = 50;

        CheckFirstTime();
    }

    /// <summary>
    /// 에너지를 증감하는 함수(0 미만으로 내려갈 수 없도록 설정됨)
    /// </summary>
    /// <param name="value">증감할 에너지 양</param>
    public void AddEnergy(int value = 1)
    {
        // 에너지 증가
        _gameData.Energy = Math.Max(0, _gameData.Energy + value);

        // 저장
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
    /// 에너지 재충전까지 남은 시간을 증감하는 함수
    /// </summary>
    /// <param name="value"></param>
    public void AddEnergyRechargeRemainingTime(int value)
    {
        // 남은 시간은 0 미만의 값으로 설정할 수 없도록
        _gameData.EnergyRechargeRemainingTime = Math.Max(0, _gameData.EnergyRechargeRemainingTime + value);
        
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
