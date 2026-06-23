using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<string, GameObject> _prefabMap = new();

    public GameObject GetPrefab(string path)
    {
        if (_prefabMap.ContainsKey(path))
            return _prefabMap[path];

        GameObject prefab = Resources.Load<GameObject>(path);

        if (prefab == null)
        {
            Debug.Log($"{path} 경로에 프리팹이 없습니다.");
        }
        else
        {
            _prefabMap[path] = prefab;
        }
        return prefab;
    }

    public T GetPrefabOnce<T>(string path) where T : Object
    {
        T prefab = Resources.Load<T>(path);

        if(prefab == null)
        {
            Debug.Log($"{path} 경로에 프리팹이 없습니다.");
        }
        return prefab;
    }
}
