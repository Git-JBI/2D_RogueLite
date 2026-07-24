using UnityEngine;

public enum AwakeningWeaponType
{
    APistol,
    AShovel,
}

public class AwakeningWeaponBase : MonoBehaviour
{
    protected AwakeningWeaponType _weaponType;
    protected HeroModel _heroModel;

    protected int _damageMagnification;

    public AwakeningWeaponType WeaponType => _weaponType;

    public virtual void Init(HeroModel model)
    {
        _heroModel = model;
    }

    public void EnableWeapon()
    {
        gameObject.SetActive(true);
    }
}