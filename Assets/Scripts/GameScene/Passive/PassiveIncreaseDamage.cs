using UnityEngine;

public class PassiveIncreaseDamage : PassiveBase
{
    protected override void CalculateStat()
    {
        _model.IncreaseDamagePercent = _passiveData.GetStatValue(PassiveStatType.DamagePercent, _level - 1);
    }
}
