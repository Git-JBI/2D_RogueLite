using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "GameDatas/StageData", fileName = "StageData")]
public class SO_StageData : ScriptableObject
{
    [SerializeField] private float _stageLimitTime;

    [SerializeField] private List<MinionController> _minions;
    [SerializeField] private List<BossController> _bosses;

    public float StageLimitTime => _stageLimitTime;

    public IReadOnlyList<MinionController> Minions => _minions;
    public IReadOnlyList<BossController> Bosses => _bosses;
}