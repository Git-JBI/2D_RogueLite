using UnityEngine;

public class PassiveIncreaseMoveSpeed : PassiveBase
{
    private void Awake()
    {
        _passiveData = Resources.Load<SO_PassiveData>("GameDatas/PassiveData/PassiveData_IncreaseMoveSpeedPercent");
    }

    protected override void CalculateStat()
    {
        _model.MoveSpeedIncreasePercent = _passiveData.GetStatValue(_level - 1);
    }
}
