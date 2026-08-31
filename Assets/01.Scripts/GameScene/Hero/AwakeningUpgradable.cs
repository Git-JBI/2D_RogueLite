using UnityEngine;

public class AwakeningUpgradable : IAwakeningUpgradable
{
    private WeaponController _weaponController;
    private SO_AwakeningConfigData _configData;

    public AwakeningUpgradable(WeaponController weaponController, SO_AwakeningConfigData configData)
    {
        _weaponController = weaponController;
        _configData = configData;
    }

    public WeaponType WeaponType => _configData.BaseWeapon;

    public AwakeningWeaponType AwakeningWeaponType => _configData.ResultWeapon;

    public Sprite IconSprite => _configData.Sprite;

    public int Level => 0;

    public int MaxLevel => 0;

    public string Desc => _configData.Desc;

    public bool IsPermanentAbility => true;


    public void Upgrade()
    {
        _weaponController.AwakeningWeapon(_configData.BaseWeapon, _configData.ResultWeapon);
    }
}
