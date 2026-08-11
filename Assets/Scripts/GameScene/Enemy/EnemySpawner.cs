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

    private GameSceneManager _gameSceneManager;
    private Transform _playerTransform;
    private SO_StageData _stageConfigData;
    private SO_ExpProbabilityConfigData _probabilities;
    private int _stage;

    private List<SO_EnemyData> _minionConfigDatas = new();
    private List<SO_EnemyData> _bossConfigDatas = new();
    private List<MinionEnemyController> _minionEnemies = new();

    private IReadOnlyList<MinionEnemyController> _minionPrefabs;
    private IReadOnlyList<BossEnemyController> _bossPrefabs;

    public event UnityAction OnEnemyDead;
    public event UnityAction OnBossDead;

    private Coroutine _spawnRoutine;

    public void Init(int stage, Transform transform, SO_StageData data, GameSceneManager gameSceneManager)
    {
        _stage = stage;
        _stageConfigData = data;
        _gameSceneManager = gameSceneManager;
        _probabilities = GameManager.Instance.ResourceManager.GetScriptable("GameDatas/ExpProbabilityData/ExpTimeConfigData") as SO_ExpProbabilityConfigData;

        _minionPrefabs = _stageConfigData.Minions;
        _bossPrefabs = _stageConfigData.Bosses;

        // 미니언 프리펩 설정 데이터 불러오기
        for(int i = 0; i < _minionPrefabs.Count; i++)
        {
            _minionConfigDatas.Add(GameManager.Instance.ResourceManager.GetScriptable($"GameDatas/EnemyConfigData/Minion/{_minionPrefabs[i].name}ConfigData") as SO_EnemyData);
        }
        // 보스 프리펩 설정 데이터 불러오기
        for(int i = 0; i < _bossPrefabs.Count; i++)
        {
            _bossConfigDatas.Add(GameManager.Instance.ResourceManager.GetScriptable($"GameDatas/EnemyConfigData/Boss/{_bossPrefabs[i].name}ConfigData") as SO_EnemyData);
        }

        _playerTransform = transform;

        _spawnRoutine = StartCoroutine(SpawnCoroutine());
    }

    public void BurstSpawnEnemy(int count, int enemyCount)
    {
        StartCoroutine(BurstSpawnCoroutine(count, enemyCount));
    }

    public void SpawnBoss()
    {
        // 일반 적 코루틴 정지
        StopCoroutine(_spawnRoutine);

        // 보스 소환 (보스리스트의 갯수 - 1)
        BossEnemyController boss = Instantiate(_bossPrefabs[_bossPrefabs.Count - 1]);

        // 보스 초기화
        boss.Init(_playerTransform, _bossConfigDatas[_bossConfigDatas.Count - 1], _stage);

        // 보스 사망 시 이벤트 처리해야할 함수 추가
        boss.OnBossDead += BossDead;
    }

    /// <summary>
    /// 적을 스폰하는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            SpawnEnemy();

            //TODO 스폰하는 시간 0.5f -> 변수로 수정
            yield return new WaitForSeconds(0.5f);
        }
    }

    /// <summary>
    /// 적을 생성하는 함수
    /// </summary>
    private void SpawnEnemy()
    {
        // _stageConfigData의 설정값에 따라 적의 종류 갯수
        int index = _random.Next(0, _stageConfigData.Minions.Count);

        GameObject enemy = GameManager.Instance.PoolManager.GetFromPool($"Prefabs/Enemies/Minion/{_minionPrefabs[index].name}", transform);

        MinionEnemyController enemyController = enemy.GetComponent<MinionEnemyController>();

        enemyController.Init(_playerTransform, _minionConfigDatas[index], _stage, GetRandomExpType());
        enemyController.OnMinionEnemyDead += EnemyDead;

        _minionEnemies.Add(enemyController);
    }

    /// <summary>
    /// 순간적으로 적을 대량 생성하는 코루틴
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
    /// 랜덤한 경험치 타입을 받는 함수
    /// </summary>
    /// <returns></returns>
    private int GetRandomExpType()
    {
        float curTime = _gameSceneManager.GetTime();
        float totalTime = _stageConfigData.StageLimitTime;

        // 전체 시간이 0 이하일 수가 없음
        if (totalTime <= 0f)
            return 0;

        int section = Mathf.Min(Mathf.FloorToInt(curTime / totalTime * 5f), 4);

        float randomValue = Random.value;

        float probabilityZero = _probabilities.GetProbability(section, 0);
        float probabilityOne = _probabilities.GetProbability(section, 1);

        if (randomValue < probabilityZero)
            return 0;
        if (randomValue < probabilityZero + probabilityOne)
            return 1;
        return 2;
    }
}