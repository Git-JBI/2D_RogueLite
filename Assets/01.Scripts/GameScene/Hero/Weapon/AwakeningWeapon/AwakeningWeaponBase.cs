using UnityEngine;

public enum AwakeningWeaponType
{
    APistol,
    AShovel,
}

public class AwakeningWeaponBase : MonoBehaviour
{
    [SerializeField] protected SO_AwakeningWeaponConfigData _weaponData;

    protected AwakeningWeaponType _weaponType;
    protected HeroModel _heroModel;

    protected float _damageMagnification;

    public AwakeningWeaponType WeaponType => _weaponType;

    public virtual void Init(HeroModel model)
    {
        _heroModel = model;

        _damageMagnification = _weaponData.DamageMagnification;
    }

    public virtual void EnableWeapon()
    {
        gameObject.SetActive(true);
    }
}