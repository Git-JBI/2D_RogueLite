using System.Collections.Generic;
using UnityEngine;

public class ResourseManager : MonoBehaviour
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
}
