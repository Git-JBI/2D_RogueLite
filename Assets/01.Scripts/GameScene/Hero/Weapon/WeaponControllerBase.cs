using UnityEngine;

public enum WeaponType
{
    Pistol,
    Rifle,
    Shotgun,
    Shovel,
    Kunai,
}

public abstract class WeaponControllerBase : MonoBehaviour, IUpgradable
{
    //TODO 추후 불러오는방법 변경 SerializeField X
    [SerializeField] protected SO_WeaponData _weaponData;
    protected InGameHeroModel _heroModel;

    protected int _level;
    protected float _damageMagnification;

    public WeaponType WeaponType => _weaponData.WeaponType;
    public Sprite IconSprite => _weaponData.WeaponIcon;
    public int Level => _level;
    public int MaxLevel => _weaponData.Descs.Length;
    public string Desc => _weaponData.Descs[_level];
    public bool IsPermanentAbility => true;

    public virtual void Init(InGameHeroModel model)
    {
        _level = 0;
        _heroModel = model;
    }
    public virtual void Upgrade()
    {
        // 활성화되지 않은 무기의 경우 켜기
        if (!gameObject.activeInHierarchy)
            EnableWeapon();

        // 레벨 증가(최초 레벨 0이라 레벨 업 후 스텟 계산)
        _level++;
        
        CalculateStats();
    }
    /// <summary>
    /// 무기 각성 시 호출할 함수
    /// </summary>
    public void AwakeningWeapon()
    {
        //TODO 이 방법 말고 다른 방법생각(_level을 99로 설정말고 bool을 넣을지 생각)
        _level = 99;
    }
    public void EnableWeapon()
    {
        gameObject.SetActive(true);
    }
    public void DisableWeapon()
    {
        gameObject.SetActive(false);
    }

    protected void CalculateStats()
    {
        _damageMagnification = _weaponData.DamageMagnification[_level];
    
        CalculateOtherStats();
    }

    protected abstract void CalculateOtherStats();
}