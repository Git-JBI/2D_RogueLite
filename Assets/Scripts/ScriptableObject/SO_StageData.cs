using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "GameDatas/StageData", fileName = "StageData")]
public class SO_StageData : ScriptableObject
{
    [SerializeField] private float _stageLimitTime;

    [SerializeField] private List<MinionEnemyController> _minions;
    [SerializeField] private List<BossEnemyController> _bosses;

    public float StageLimitTime => _stageLimitTime;

    public IReadOnlyList<MinionEnemyController> Minions => _minions;
    public IReadOnlyList<BossEnemyController> Bosses => _bosses;
}