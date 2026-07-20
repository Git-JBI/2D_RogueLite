using UnityEngine;

public class PassiveIncreaseMoveSpeed : PassiveBase
{
    private void Awake()
    {
        _passiveData = Resources.Load<PassiveData>("GameDatas/PassiveData/PassiveData_IncreaseMoveSpeedPercent");
    }

    protected override void CalculateStat()
    {
        _model.PassiveMoveSpeedPercent = _passiveData.GetStatValue(_level - 1);
    }
}
