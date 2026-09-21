using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroWeaponController : MonoBehaviour
{
    private WeaponControllerBase[] _weapons;
    private AwakeningWeaponBase[] _awakeningWeapons;
    private IReadOnlyList<IUpgradable> _upgradables;

    public IReadOnlyList<IUpgradable> Upgradables => _upgradables;

    private void Awake()
    {
        _weapons = GetComponentsInChildren<WeaponControllerBase>(true);
        _awakeningWeapons = GetComponentsInChildren<AwakeningWeaponBase>(true);
        _upgradables = GetComponentsInChildren<IUpgradable>(true);
    }

    private void Update()
    {
        //TODO 나중에 제거
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _weapons[4].Upgrade();
    }

    public void Init(HeroModel model)
    {
        // 일반 무기 초기화
        foreach (WeaponControllerBase weapon in _weapons)
        {
            weapon.Init(model);
        }
        // 각성 무기 초기화
        foreach (AwakeningWeaponBase weapon in _awakeningWeapons)
        {
            weapon.Init(model);
        }

        //TODO 장비마다 다른 시작 무기 설정 해야함
        _weapons[0].Upgrade();
    }

    public IUpgradable GetIUpgradableWeapon(WeaponType type)
    {
        return _weapons.First(x => x.WeaponType == type);
    }

    /// <summary>
    /// 기존의 무기를 비활성화 하고 각성하는 무기를 활성화 하는 함수
    /// </summary>
    /// <param name="weaponType">기존 무기 타입</param>
    /// <param name="awakeningWeaponType">각성 무기 타입</param>
    public void AwakeningWeapon(WeaponType weaponType, AwakeningWeaponType awakeningWeaponType)
    {
        WeaponControllerBase weapon = _weapons.First(x => x.WeaponType == weaponType);
        AwakeningWeaponBase aWeapon = _awakeningWeapons.First(x => x.WeaponType == awakeningWeaponType);

        // 기존 무기 각성
        weapon.AwakeningWeapon();
        // 기존 무기 비활성화
        weapon.DisableWeapon();
        // 각성 무기 활성화
        aWeapon.EnableWeapon();
    }
}