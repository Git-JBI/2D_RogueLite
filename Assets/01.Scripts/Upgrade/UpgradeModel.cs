using UnityEngine;

public enum UpgradeType
{
    Damage,
    Hp,
    Armor,
}

public enum UpgradeReturnType
{
    MaxLevel,
    NoGold,
    Success,
}

public class UpgradeModel
{
    private GameData _gameData;
    private GameModel _gameModel;

    private UpgradeData _damageData;
    private UpgradeData _hpData;
    private UpgradeData _armorData;

    public UpgradeModel(GameData data, GameModel model)
    {
        _gameData = data;
        _gameModel = model;

        _damageData = Resources.Load<UpgradeData>("GameDatas/UpgradeData/UpgradeDataDamage");
        _hpData = Resources.Load<UpgradeData>("GameDatas/UpgradeData/UpgradeDataHp");
        _armorData = Resources.Load<UpgradeData>("GameDatas/UpgradeData/UpgradeDataArmor");
    }

    public int DamageUpgradeMaxLevel => _damageData.Value.Length - 1;
    public int HpUpgradeMaxLevel => _hpData.Value.Length - 1;
    public int ArmorUpgradeMaxLevel => _armorData.Value.Length - 1;


    /// <summary>
    /// type에 따른 업그레이드 레벨 반환 함수
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetLevel(UpgradeType type)
    {
        return _gameData.UpgradeLevel[(int)type];
    }
    /// <summary>
    /// type에 따른 업그레이드 가격 반환 함수
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetPrice(UpgradeType type)
    {
        UpgradeData data = GetData(type);
        int level = GetLevel(type);

        return level >= data.Value.Length - 1 ? -1 : data.Price[level];
    }
    /// <summary>
    /// type에 따른 업그레이드 Value 반환 함수
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public float GetValue(UpgradeType type)
    {
        return GetData(type).Value[GetLevel(type)];
    }
    /// <summary>
    /// type에 따른 Upgrade를 실행하고 UpgradeReturnType을 반환하는 함수
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public UpgradeReturnType TryUpgrade(UpgradeType type)
    {
        UpgradeData data = GetData(type);
        int level = GetLevel(type);

        if (level >= data.Value.Length - 1)
            return UpgradeReturnType.MaxLevel;

        int price = data.Price[level];

        if (price > _gameData.Gold)
            return UpgradeReturnType.NoGold;

        _gameModel.AddGold(-price);
        _gameData.UpgradeLevel[(int)type]++;

        _gameModel.SaveGameData();

        return UpgradeReturnType.Success;
    }

    /// <summary>
    /// type에 따른 UpgradeData 반환 함수
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private UpgradeData GetData(UpgradeType type)
    {
        switch(type)
        {
            case UpgradeType.Damage:
                return _damageData;
            case UpgradeType.Hp:
                return _hpData;
            case UpgradeType.Armor:
                return _armorData;
            default:
                return null;
        }
    }
}
