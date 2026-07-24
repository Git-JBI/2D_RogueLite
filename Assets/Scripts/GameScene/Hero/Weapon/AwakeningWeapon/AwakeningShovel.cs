using UnityEngine;

public class AwakeningShovel : AwakeningWeaponBase
{
    public override void Init(HeroModel model)
    {
        base.Init(model);

        _weaponType = AwakeningWeaponType.AShovel;
    }

    public void OnEnable()
    {
        Debug.Log("»ð °¢¼º");
    }
}
