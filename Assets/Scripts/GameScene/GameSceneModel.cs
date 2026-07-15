public class GameSceneModel
{
    private int _enemyDeadCount;
    private int _stageGold;
    private int _stage;
    private float _stageTime;

    public int EnemyDeadCount => _enemyDeadCount;
    public int StageGold => _stageGold;
    public int Stage => _stage;
    public float StageTime => _stageTime;

    public GameSceneModel(int stage)
    {
        _enemyDeadCount = 0;
        _stageGold = 0;
        _stage = stage;
        _stageTime = 0;
    }

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
