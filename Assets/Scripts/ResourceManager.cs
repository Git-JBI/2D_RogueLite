using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<string, GameObject> _prefabMap = new();
    private Dictionary<string, ScriptableObject> _scriptableMap = new();

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

    public ScriptableObject GetScriptable(string path)
    {
        if(_scriptableMap.ContainsKey(path))
            return _scriptableMap[path];

        ScriptableObject scriptable = Resources.Load<ScriptableObject>(path);

        if (scriptable == null)
            Debug.Log($"{path} 경로에 ScriptableObject가 없습니다.");
        else
            _scriptableMap[path] = scriptable;
        return scriptable;
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
