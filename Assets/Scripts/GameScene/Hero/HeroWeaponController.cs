using UnityEngine;

public class HeroWeaponController : MonoBehaviour
{
    private WeaponShovel _shovel;

    private void Awake()
    {
        _shovel = GetComponentInChildren<WeaponShovel>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            _shovel.LevelUp();
        }
    }
}
