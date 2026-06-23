using UnityEngine;

public class PassiveIncreaseMoveSpeed : PassiveBase
{
    protected override void CalculateStat()
    {
        _model.IncreaseMoveSpeedPercent = _passiveData.GetStatValue(PassiveStatType.MoveSpeedPercent, _level - 1);
    }
}
