using UnityEngine;

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
    /// 데미지 업그레이드 레벨 반환 함수
    /// </summary>
    /// <returns>DamageUpgradeLevel</returns>
    public int GetDamageUpgradeLevel()
    {
        return _gameData.UpgradeLevel[0];
    }
    /// <summary>
    /// 체력 업그레이드 레벨 반환 함수
    /// </summary>
    /// <returns>HpUpgradeLevel</returns>
    public int GetHpUpgradeLevel()
    {
        return _gameData.UpgradeLevel[1];
    }
    /// <summary>
    /// 방어력 업그레이드 레벨 반환 함수
    /// </summary>
    /// <returns>ArmorUpgradeLevel</returns>
    public int GetArmorUpgradeLevel()
    {
        return _gameData.UpgradeLevel[2];
    }

    /// <summary>
    /// 현재 데미지 업그레이드 레벨에 따른 필요 업그레이드 금액 반환 함수
    /// </summary>
    /// <returns></returns>
    public int GetDamageUpgradePrice()
    {
        // 업그레이드 레벨
        int level = _gameData.UpgradeLevel[0];

        if (level == DamageUpgradeMaxLevel)
            // 최대 레벨일 시 -1 반환
            return -1;
        else
            return _damageData.Price[level];
    }
    /// <summary>
    /// 현재 체력 업그레이드 레벨에 따른 필요 업그레이드 금액 반환 함수
    /// </summary>
    /// <returns></returns>
    public int GetHpUpgradePrice()
    {
        // 업그레이드 레벨
        int level = _gameData.UpgradeLevel[1];

        if (level == HpUpgradeMaxLevel)
            // 최대 레벨일 시 -1 반환
            return -1;
        else
            return _hpData.Price[level];
    }
    /// <summary>
    /// 현재 방어력 업그레이드 레벨에 따른 필요 업그레이드 금액 반환 함수
    /// </summary>
    /// <returns></returns>
    public int GetArmorUpgradePrice()
    {
        // 업그레이드 레벨
        int level = _gameData.UpgradeLevel[2];

        if (level == ArmorUpgradeMaxLevel)
            // 최대 레벨일 시 -1 반환
            return -1;
        else
            return _armorData.Price[level];
    }
    /// <summary>
    /// 현재 데미지 업그레이드 레벨에 따른 가치 반환 함수
    /// </summary>
    /// <returns></returns>
    public float GetUpgradeDamageValue()
    {
        return _damageData.Value[_gameData.UpgradeLevel[0]];
    }
    /// <summary>
    /// 현재 체력 업그레이드 레벨에 따른 가치 반환 함수
    /// </summary>
    /// <returns></returns>
    public float GetUpgradeHpValue()
    {
        return _hpData.Value[_gameData.UpgradeLevel[1]];
    }
    /// <summary>
    /// 현재 방어력 업그레이드 레벨에 따른 가치 반환 함수
    /// </summary>
    /// <returns></returns>
    public float GetUpgradeArmorValue()
    {
        return _armorData.Value[_gameData.UpgradeLevel[2]];
    }

    public UpgradeReturnType DamageUpgradeLevelUp()
    {
        // 현재 데미지업그레이드 레벨
        int damageUpgradeLevel = _gameData.UpgradeLevel[0];

        // 업그레이드 레벨이 최대일 경우 리턴
        if (damageUpgradeLevel == DamageUpgradeMaxLevel)
            return UpgradeReturnType.MaxLevel;

        // 레벨업에 필요한 금액이 없을 시 리턴
        if (_damageData.Price[damageUpgradeLevel] > _gameData.Gold)
            return UpgradeReturnType.NoGold;

        // 금액이 있는 경우 골드 차감
        _gameModel.AddGold(-_damageData.Price[damageUpgradeLevel]);

        // 업그레이드 레벨 증가
        _gameData.UpgradeLevel[0]++;

        // 저장
        _gameModel.SaveGameData();

        return UpgradeReturnType.Success;
    }

    public UpgradeReturnType HpUpgradeLevelUp()
    {
        // 현재 Hp업그레이드 레벨
        int hpUpgradeLevel = _gameData.UpgradeLevel[1];

        // 업그레이드 레벨이 최대일 경우 리턴
        if (hpUpgradeLevel == HpUpgradeMaxLevel)
            return UpgradeReturnType.MaxLevel;

        // 레벨업에 필요한 금액이 없을 시 리턴
        if (_hpData.Price[hpUpgradeLevel] > _gameData.Gold)
            return UpgradeReturnType.NoGold;

        // 금액이 있는 경우 골드 차감
        _gameModel.AddGold(-_hpData.Price[hpUpgradeLevel]);

        // 업그레이드 레벨 증가
        _gameData.UpgradeLevel[1]++;

        // 저장
        _gameModel.SaveGameData();

        return UpgradeReturnType.Success;
    }

    public UpgradeReturnType ArmorUpgradeLevelUp()
    {
        // 현재 Armor업그레이드 레벨
        int armorUpgradeLevel = _gameData.UpgradeLevel[2];

        // 업그레이드 레벨이 최대일 경우 리턴
        if (armorUpgradeLevel == ArmorUpgradeMaxLevel)
            return UpgradeReturnType.MaxLevel;

        // 레벨업에 필요한 금액이 없을 시 리턴
        if (_armorData.Price[armorUpgradeLevel] > _gameData.Gold)
            return UpgradeReturnType.NoGold;

        // 금액이 있는 경우 골드 차감
        _gameModel.AddGold(-_armorData.Price[armorUpgradeLevel]);

        // 업그레이드 레벨 증가
        _gameData.UpgradeLevel[2]++;

        // 저장
        _gameModel.SaveGameData();

        return UpgradeReturnType.Success;
    }
}
