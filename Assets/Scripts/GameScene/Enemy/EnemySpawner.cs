using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 적을 스폰하는 클래스
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    private System.Random _random = new System.Random();

    private GameSceneModel _gameSceneModel;
    private Transform _playerTransform;
    private SO_StageData _stageConfigData;
    private int _stage;

    private List<SO_EnemyData> _minionsConfigData = new();
    private List<SO_EnemyData> _bossConfigData = new();
    private List<MinionEnemyController> _minionEnemies = new();

    private IReadOnlyList<MinionEnemyController> _minionEnemiePrefabs;
    private IReadOnlyList<BossEnemyController> _bossEnemiePrefabs;

    public event UnityAction OnEnemyDead;
    public event UnityAction OnBossDead;

    private Coroutine _spawnRoutine;
    private Coroutine _burstSpawnRoutine;

    public void Init(int stage, Transform transform, SO_StageData data)
    {
        _stage = stage;
        _stageConfigData = data;
        _minionEnemiePrefabs = _stageConfigData.MinionEnemies;
        _bossEnemiePrefabs = _stageConfigData.BossEnemies;

        for(int i = 0; i < _minionEnemiePrefabs.Count; i++)
        {
            _minionsConfigData.Add(GameManager.Instance.ResourceManager.GetScriptable($"GameDatas/EnemyConfigData/Minion/{_minionEnemiePrefabs[i].name}ConfigData") as SO_EnemyData);
        }
        //TODO 보스 데이터 받아오기
        // for(int i = 0; i < _)

        _playerTransform = transform;

        _spawnRoutine = StartCoroutine(SpawnCoroutine());
    }

    public void BurstSpawnEnemy(int count, int enemyCount)
    {
        _burstSpawnRoutine = StartCoroutine(BurstSpawnCoroutine(count, enemyCount));
    }

    public void SpawnBoss()
    {
        // 일반 적 코루틴 정지
        StopCoroutine(_spawnRoutine);

        // 보스 소환 (보스리스트의 갯수 - 1)
        BossEnemyController boss = Instantiate(_bossEnemiePrefabs[_bossEnemiePrefabs.Count - 1]);

        // 보스 초기화
        boss.Init(_playerTransform, RandomSpawnPosition());

        // 보스 사망 시 이벤트 처리해야할 함수 추가
        boss.OnBossDead += BossDead;
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(0.5f);
        }
    }

    /// <summary>
    /// 적을 생성하는 함수
    /// </summary>
    private void SpawnEnemy()
    {
        // _stageConfigData의 설정값에 따라 적의 종류 갯수
        int index = _random.Next(0, _stageConfigData.MinionEnemies.Count);

        GameObject enemy = GameManager.Instance.PoolManager.GetFromPool($"Prefabs/Enemies/Minion/{_minionEnemiePrefabs[index].name}", transform);

        MinionEnemyController enemyController = enemy.GetComponent<MinionEnemyController>();

        enemyController.Init(_playerTransform, RandomSpawnPosition(), _minionsConfigData[index], _stage);
        enemyController.OnMinionEnemyDead += EnemyDead;

        _minionEnemies.Add(enemyController);
    }

    /// <summary>
    /// 순간적으로 적을 대량 생성하는 함수
    /// </summary>
    /// <param name="count"></param>
    /// <param name="enemyCount"></param>
    /// <returns></returns>
    private IEnumerator BurstSpawnCoroutine(int count, int enemyCount)
    {
        for(int i = 0; i < count; i++)
        {
            for(int j = 0; j < enemyCount; j++)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(3.0f);
        }
        StopCoroutine(_burstSpawnRoutine);
    }

    /// <summary>
    /// 적 사망 시 호출할 함수
    /// </summary>
    private void EnemyDead(MinionEnemyController enemyController)
    {
        // 스폰된 적 리스트에서 제거
        _minionEnemies.Remove(enemyController);

        // 적 죽을 시 이벤트 발행
        OnEnemyDead?.Invoke();
    }

    /// <summary>
    /// 보스 사망 시 호출할 함수
    /// </summary>
    private void BossDead()
    {
        OnBossDead?.Invoke();
    }

    /// <summary>
    /// 플레이어 위치 기준으로 랜덤한 위치를 반환하는 함수
    /// </summary>
    /// <returns></returns>
    private Vector2 RandomSpawnPosition()
    {
        Vector2 pos = _playerTransform.position;

        pos += Random.insideUnitCircle.normalized * 10.0f;

        return pos;
    }
    /*
     * public static class TimeWeightedRandom
{
    // 행: 시간 구간, 열: 0, 1, 2가 나올 확률
    private static readonly float[,] Probabilities =
    {
        { 0.8f, 0.2f, 0.0f }, // 1구간
        { 0.6f, 0.4f, 0.0f }, // 2구간
        { 0.4f, 0.4f, 0.2f }, // 3구간
        { 0.2f, 0.3f, 0.5f }, // 4구간
        { 0.0f, 0.2f, 0.8f }  // 5구간
    };

    public static int GetRandomValue(float elapsedTime, float totalTime)
    {
        if (totalTime <= 0f)
        {
            Debug.LogError("전체 시간은 0보다 커야 합니다.");
            return 0;
        }

        // 경과 시간을 0~전체 시간 안으로 제한
        elapsedTime = Mathf.Clamp(elapsedTime, 0f, totalTime);

        // 시간을 0~1로 정규화한 뒤 5개 구간으로 변환
        int section = Mathf.Min(
            Mathf.FloorToInt(elapsedTime / totalTime * 5f),
            4
        );

        float randomValue = Random.value;

        float probabilityOfZero = Probabilities[section, 0];
        float probabilityOfOne = Probabilities[section, 1];

        if (randomValue < probabilityOfZero)
            return 0;

        if (randomValue < probabilityOfZero + probabilityOfOne)
            return 1;

        return 2;
    }
}
     */
}