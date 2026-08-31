using UnityEngine;

public class UpgradeModel
{
    private GameData _gameData;
    private GameModel _gameModel;

    private UpgradeData _damageData;
    private UpgradeData _hpData;
    private UpgradeData _armorData;

    private int[] _upgradeLevel;

    public int[] UpgradeLevel => _upgradeLevel;

    public UpgradeModel(GameData data, GameModel model)
    {
        _gameData = data;
        _gameModel = model;

        _damageData = Resources.Load<UpgradeData>("GameDatas/UpgradeData/UpgradeDataDamage");
        _hpData = Resources.Load<UpgradeData>("GameDatas/UpgradeData/UpgradeDataHp");
        _armorData = Resources.Load<UpgradeData>("GameDatas/UpgradeData/UpgradeDataArmor");

        _upgradeLevel = _gameData.UpgradeLevel;
    }

    public float GetUpgradeDamageValue()
    {
        return _damageData.Value[_gameModel.UpgradeLevel[0]];
    }
    public float GetUpgradeHpValue()
    {
        return _hpData.Value[_gameModel.UpgradeLevel[1]];
    }
    public float GetUpgradeArmorValue()
    {
        return _armorData.Value[_gameModel.UpgradeLevel[2]];
    }

    public void LevelUpUpgradeDamage()
    {
        // 업그레이드 레벨이 설정 데이터 값 길이보다 낮은 경우
        if (_upgradeLevel[0] <= _damageData.Value.Length)
            _upgradeLevel[0]++;
    }

    public void LevelUpUpgradeHp()
    {
        // 업그레이드 레벨이 설정 데이터 값 길이보다 낮은 경우
        if (_upgradeLevel[1] <= _hpData.Value.Length)
            _upgradeLevel[1]++;
    }

    public void LevelUpUpgradeArmor()
    {
        // 업그레이드 레벨이 설정 데이터 값 길이보다 낮은 경우
        if (_upgradeLevel[2] <= _armorData.Value.Length)
            _upgradeLevel[2]++;
    }
}
