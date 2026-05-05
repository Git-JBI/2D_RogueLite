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

    //TODO 업그레이드쪽으로 이동
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1");
            _shovel.Upgrade();
        }

        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            _pistol.Upgrade();
        }
    }

    public void Init(HeroModel model)
    {
        _heromodel = model;

        _shovel.Init(model);
        _pistol.Init(model);
        _rifle.Init(model);
        _shotgun.Init(model);
    }
}
