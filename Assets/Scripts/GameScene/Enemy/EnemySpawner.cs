using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private EnemyController _enemyPrefab;   //TODO GameSceneManager에서 받아오기
    [SerializeField]
    private HeroController _hero;           //TODO GameSceneManager에서 받아오기

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            Instantiate(_enemyPrefab, RandomSpawnPosition(), Quaternion.identity);

            yield return new WaitForSeconds(2.0f);
        }
    }

    private Vector2 RandomSpawnPosition()
    {
        Vector2 pos = _hero.transform.position;

        pos += Random.insideUnitCircle.normalized * 5.0f;

        return pos;
    }
}