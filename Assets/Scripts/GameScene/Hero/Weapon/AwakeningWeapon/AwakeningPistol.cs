using UnityEngine;

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
