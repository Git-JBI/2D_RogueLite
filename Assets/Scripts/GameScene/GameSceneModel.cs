using UnityEngine;

public class GameSceneModel : MonoBehaviour
{
    private int _enemyDeadCount;
    private float _stageTime;

    public int EnemyDeadCount => _enemyDeadCount;

    public float StageTime => _stageTime;

    public void IncreaseEnemyDeadCount(int count)
    {
        _enemyDeadCount += count;
    }

    public void IncreaseStageTime(float time)
    {
        _stageTime += time;
    }
}
