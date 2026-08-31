using UnityEngine;

public class PassiveIncreaseDamage : PassiveBase
{
    private void Awake()
    {
        _passiveData = Resources.Load<SO_PassiveData>("GameDatas/PassiveData/PassiveData_IncreaseDamagePercent");
    }

    protected override void CalculateStat()
    {
        _model.PassiveDamagePercent = _passiveData.GetStatValue(_level - 1);
    }
}
