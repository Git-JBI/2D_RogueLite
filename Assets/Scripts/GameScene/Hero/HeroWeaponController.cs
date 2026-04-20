using UnityEngine;

public class HeroWeaponController : MonoBehaviour
{
    private WeaponShovel _shovel;
    private WeaponPistol _pistol;

    private void Awake()
    {
        _shovel = GetComponentInChildren<WeaponShovel>();
        _pistol = GetComponentInChildren<WeaponPistol>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            _shovel.LevelUp();
        }

        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            _pistol.LevelUp();
        }
    }
}
