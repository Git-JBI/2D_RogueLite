using UnityEngine;

public enum WeaponName
{
    Pistol,
    Rifle,
    Shotgun,
    Shovel,
}

public abstract class WeaponBase : MonoBehaviour, IUpgradable
{
    //TODO 추후 불러오는방법 변경 SerializeField X
    [SerializeField] protected SO_WeaponData _weaponData;
    protected HeroModel _heroModel;

    protected int _level;
    protected float _damageMagnification;

    public WeaponName WeaponName => _weaponData.WeaponName;
    public Sprite IconSprite => _weaponData.WeaponIcon;
    public int Level => _level;
    public int MaxLevel => _weaponData.Descs.Length;
    public string Desc => _weaponData.Descs[_level];
    public bool IsPermanentAbility => true;

    protected virtual void Awake()
    {
        _level = 0;
    }

    public void Init(HeroModel model)
    {
        _heroModel = model;
    }

    protected virtual void CalculateStats()
    {
        _damageMagnification = _weaponData.GetStatValue(WeaponStatType.DamageMagnification, _level - 1);
    }

    public virtual void Upgrade()
    {
        // 활성화되지 않은 무기의 경우 켜기
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);

        // 레벨 증가(최초 레벨 0이라 레벨 업 후 스텟 계산)
        _level++;
        
        CalculateStats();
    }
}