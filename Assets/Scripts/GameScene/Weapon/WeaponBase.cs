using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IUpgradable
{
    protected WeaponData _weaponData;

    protected int _level;
    protected float _damage;

    public Sprite IconSprite => _weaponData.WeaponIcon;

    public int Level => _level;

    public string Desc => _weaponData.Descs[_level];

    protected abstract void CalculateStats();

    public virtual void Upgrade()
    {
        _level++;
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
        CalculateStats();
    }
}