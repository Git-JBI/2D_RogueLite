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

        Debug.Log(_weaponData.name);
        Debug.Log(_weaponData.StatDatas[0].GetValue(0));
        Debug.Log(_weaponData.StatDatas[1].GetValue(0));
        Debug.Log(_weaponData.StatDatas[2].GetValue(0));

        _damageMagnification = _weaponData.GetStatValue(WeaponStatType.DamageMagnification);
    }

    public virtual void EnableWeapon()
    {
        gameObject.SetActive(true);
    }
}