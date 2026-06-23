using UnityEngine;

public class PassiveIncreaseMaxHp : PassiveBase
{
    protected override void CalculateStat()
    {
        _model.IncreaseMaxHpPercent = _passiveData.GetStatValue(PassiveStatType.MaxHpPercent, _level - 1);
    }
}
