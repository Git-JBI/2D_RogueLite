using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private WeaponBase[] _weapons;

    public WeaponBase[] Weapons => _weapons;

    private void Awake()
    {
        _weapons = GetComponentsInChildren<WeaponBase>(true);
    }

    public void Init(HeroModel model)
    {
        foreach (WeaponBase weapon in _weapons)
        {
            weapon.Init(model);
        }

        //TODO 장비마다 다른 시작 무기 설정 해야함
        _weapons[0].Upgrade();
    }
}