using UnityEngine;

public class PassiveIncreaseMaxHp : PassiveBase
{
    private void Awake()
    {
        _passiveData = Resources.Load<SO_PassiveData>("GameDatas/PassiveData/PassiveData_IncreaseMaxHpPercent");
    }

    protected override void CalculateStat()
    {
        _model.PassiveMaxHpPercent = _passiveData.GetStatValue(_level - 1);
    }
}
