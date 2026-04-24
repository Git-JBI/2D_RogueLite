using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private Dictionary<string, Pool> _poolMap = new();

    public Pool GetPool(string prefabPath, int poolSize = 20)
    {
        if(_poolMap.ContainsKey(prefabPath) == false)
        {
            GameObject prefab = GameManager.Instance.ResourceManager.GetPrefab(prefabPath);

            if (prefab == null)
                return null;

            GameObject parantGo = new GameObject($"Pool_{prefabPath}");

            DontDestroyOnLoad(parantGo);

            Pool pool = new Pool(prefab, parantGo.transform, poolSize);

            _poolMap[prefabPath] = pool;
        }

        return _poolMap[prefabPath];
    }
    
    public GameObject GetFromPool(string prefabPath)
    {
        Pool pool = GetPool(prefabPath);

        if (pool == null)
            return null;

        return pool.Pop();
    }
}
