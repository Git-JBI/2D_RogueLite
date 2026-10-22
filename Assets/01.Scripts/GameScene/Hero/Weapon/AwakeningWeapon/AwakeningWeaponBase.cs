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
    protected InGameHeroModel _heroModel;

    protected float _damageMagnification;

    public AwakeningWeaponType WeaponType => _weaponType;

    public virtual void Init(InGameHeroModel model)
    {
        _heroModel = model;

        _damageMagnification = _weaponData.DamageMagnification;
    }

    public virtual void EnableWeapon()
    {
        gameObject.SetActive(true);
    }
}