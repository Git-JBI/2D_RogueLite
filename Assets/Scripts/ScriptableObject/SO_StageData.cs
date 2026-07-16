using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "GameDatas/StageData", fileName = "StageData")]
public class SO_StageData : ScriptableObject
{
    [SerializeField] private float _stagePlayTime;

    [SerializeField] private List<MinionEnemyController> _minionEnemies;
    [SerializeField] private List<BossEnemyController> _bossEnemies;

    public float StagePlayTime => _stagePlayTime;

    public IReadOnlyList<MinionEnemyController> MinionEnemies => _minionEnemies;
    public IReadOnlyList<BossEnemyController> BossEnemies => _bossEnemies;
}