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
        _curHp = _maxHp;
        _damage = CalculateStat(data.Damage, stage);
        _moveSpeed = data.MoveSpeed;
    }

    public void TakeDamage(float damage)
    {
        _curHp = Mathf.Clamp(_curHp - damage, 0, _maxHp);
    }
    
    /// <summary>
    /// 적의 스텟 데이터에 스테이지에 따른 배율을 넣는 함수
    /// </summary>
    /// <param name="baseValue">기존 값</param>
    /// <param name="stage">현재 진행하는 스테이지</param>
    /// <returns>기존 값 * 배율</returns>
    private float CalculateStat(float baseValue, int stage)
    {
        // 스테이지 입력이 0이하인 경우
        if (stage <= 0)
            stage = 1;

        float bonus1 = 1.0f + (stage * 0.1f);

        int tier10 = stage / 10;
        float bonus10 = 1.0f + (tier10 * 0.1f);

        int tier100 = stage / 100;
        float bonus100 = 1.0f + (tier100 * 0.1f);

        float value = baseValue * bonus1 * bonus10 * bonus100;

        return Mathf.RoundToInt(value);
    }
}
