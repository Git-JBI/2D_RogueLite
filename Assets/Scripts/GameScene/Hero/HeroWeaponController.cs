using UnityEngine;

public class HeroWeaponController : MonoBehaviour
{
    private WeaponShovel _shovel;
    private WeaponPistol _pistol;

    private void Awake()
    {
        _shovel = GetComponentInChildren<WeaponShovel>(true);
        _pistol = GetComponentInChildren<WeaponPistol>(true);
    }

    //TODO 업그레이드쪽으로 이동
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            _shovel.Upgrade();
        }

        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            _pistol.Upgrade();
        }
    }
}
