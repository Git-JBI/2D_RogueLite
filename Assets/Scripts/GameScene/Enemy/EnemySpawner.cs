using System.Collections;
using UnityEngine;

/// <summary>
/// 적을 스폰
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private EnemyController _enemyPrefab;   //TODO GameSceneManager에서 받아오기
    [SerializeField]
    private HeroController _hero;           //TODO GameSceneManager에서 받아오기
    [SerializeField]
    private UIManager _ui;

    private int _deadCount;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            GameObject enemy;

            //TODO PoolManager 사용
            // enemy = Instantiate(_enemyPrefab, RandomSpawnPosition(), Quaternion.identity);

            enemy = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Enemies/Enemy", transform);
            
            EnemyController enemyController = enemy.GetComponent<EnemyController>();

            enemyController.Init(RandomSpawnPosition());
            enemyController.OnDead += EnemyDead;

            yield return new WaitForSeconds(2.0f);
        }
    }

    private void EnemyDead()
    {
        _deadCount++;
        _ui.IncreaseDeadCount(_deadCount);
    }

    private Vector2 RandomSpawnPosition()
    {
        Vector2 pos = _hero.transform.position;

        pos += Random.insideUnitCircle.normalized * 5.0f;

        return pos;
    }
}