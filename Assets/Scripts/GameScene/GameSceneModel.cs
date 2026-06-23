public class GameSceneModel
{
    private int _enemyDeadCount;
    private int _stageGold;
    private float _stageTime;

    public int EnemyDeadCount => _enemyDeadCount;
    public int StageGold => _stageGold;

    public float StageTime => _stageTime;

    public void IncreaseEnemyDeadCount(int count)
    {
        _enemyDeadCount += count;
    }

    public void IncreaseStageGold(int value)
    {
        _stageGold += value;
    }

    public void IncreaseStageTime(float time)
    {
        _stageTime += time;
    }
}
