using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IUpgradable
{
    //TODO 추후 불러오는방법 변경 SerializeField X
    [SerializeField] protected WeaponData _weaponData;

    protected int _level;
    protected float _damage;

    public Sprite IconSprite => _weaponData.WeaponIcon;

    public int Level => _level;

    public string Desc => _weaponData.Descs[_level];

    public bool IsPermanentAbility => true;

    protected virtual void Awake()
    {
        _level = 0;
    }

    protected abstract void CalculateStats();

    public virtual void Upgrade()
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);

        if (_level == 5)
            return;
        _level++;
        
        CalculateStats();
    }
}