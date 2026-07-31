using UnityEngine.Events;

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

    /// <summary>
    /// T0 : 사망한 적 숫자
    /// GameSceneManager.Start()
    /// </summary>
    public UnityAction<int> OnIncreaseEnemyDeadCount;

    public GameSceneModel()
    {
        _enemyDeadCount = 0;
        _stageGold = 0;
        _stageTime = 0;
    }

    public void IncreaseEnemyDeadCount()
    {
        _enemyDeadCount++;
        OnIncreaseEnemyDeadCount?.Invoke(_enemyDeadCount);
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
