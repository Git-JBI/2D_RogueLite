using UnityEngine;

/// <summary>
/// 적을 튕겨가며 공격하는 총알을 발사하는 각성 피스톨
/// </summary>
public class AwakeningPistol : AwakeningWeaponBase
{
    public override void Init(HeroModel model)
    {
        base.Init(model);
        _weaponType = AwakeningWeaponType.APistol;
    }

    private void OnEnable()
    {
        Debug.Log("피스톨 각성");
    }
}