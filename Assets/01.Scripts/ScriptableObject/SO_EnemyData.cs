using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/EnemyData", fileName = "ConfigData")]

public class SO_EnemyData : ScriptableObject
{
    [SerializeField] private float _maxHp;
    [SerializeField] private float _damage;
    [SerializeField] private float _moveSpeed;

    public float MaxHp => _maxHp;
    public float Damage => _damage;
    public float MoveSpeed => _moveSpeed;
}
