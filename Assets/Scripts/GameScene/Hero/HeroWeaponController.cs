using UnityEngine;

public class HeroWeaponController : MonoBehaviour
{
    private WeaponShovel _shovel;
    private WeaponPistol _pistol;
    private WeaponRifle _rifle;
    private WeaponShotgun _shotgun;

    private HeroModel _heromodel;

    private void Awake()
    {
        _shovel = GetComponentInChildren<WeaponShovel>(true);
        _pistol = GetComponentInChildren<WeaponPistol>(true);
        _rifle = GetComponentInChildren<WeaponRifle>(true);
        _shotgun = GetComponentInChildren<WeaponShotgun>(true);
    }

    public void Init(HeroModel model)
    {
        _heromodel = model;

        _shovel.Init(model);
        _pistol.Init(model);
        _rifle.Init(model);
        _shotgun.Init(model);

        //TODO 장비마다 다른 시작 무기
        _shovel.Upgrade();
    }
}
