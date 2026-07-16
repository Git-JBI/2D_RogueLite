using UnityEngine;

public class EnemyModel
{
    private float _maxHp;
    private float _curHp;
    private float _damage;
    private float _moveSpeed;

    public float CurHp => _curHp;
    public float Damage => _damage;
    public float MoveSpeed => _moveSpeed;

    public EnemyModel(SO_EnemyData data, int stage)
    {
        _maxHp = CalculateStat(data.MaxHp, stage);
        _curHp = CalculateStat(data.MaxHp, stage);
        _damage = CalculateStat(data.Damage, stage);
        _moveSpeed = data.MoveSpeed;
    }

    public void TakeDamage(float damage)
    {
        _curHp = Mathf.Clamp(_curHp - damage, 0, _maxHp);
    }
    
    private float CalculateStat(float baseValue, int stage)
    {
        if (stage <= 0)
            stage = 1;
        float stagemulValue = baseValue * stage;

        float bonusMul = 1.0f;
        if(stage >= 10)
        {
            int bonus = stage / 10;
            bonusMul += bonus * 0.1f;
        }

        float value = stagemulValue * bonusMul;

        return Mathf.RoundToInt(value);
    }
}
