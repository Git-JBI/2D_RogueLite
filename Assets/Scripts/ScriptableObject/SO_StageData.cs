using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "GameDatas/StageData", fileName = "StageData")]
public class SO_StageData : ScriptableObject
{
    private float _stagePlayTime;
    
    private List<NormalEnemyController> _normalEnemies = new();
    private List<BossEnemyController> _bossEnemies = new();

    public float StagePlayTime => _stagePlayTime;

    public IReadOnlyList<NormalEnemyController> NormalEnemies => _normalEnemies;
    public IReadOnlyList<BossEnemyController> BossEnemies => _bossEnemies;
}
