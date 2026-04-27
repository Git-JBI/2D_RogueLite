using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private Stack<GameObject> _pool;
    private GameObject _prefab;
    private Transform _parent;

    /// <summary>
    /// Pool 생성자
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <param name="initStack"></param>
    public Pool(GameObject prefab, Transform parent, int initStack)
    {
        _prefab = prefab;
        _parent = parent;
        _pool = new Stack<GameObject>(initStack);

        for(int i = 0; i < initStack; i++)
        {
            CreateObject();
        }
    }

    private void CreateObject()
    {
        // 새 게임오브젝트 생성
        GameObject go = Object.Instantiate(_prefab);

        // 게임오브젝트의 부모를 풀로 설정
        go.transform.SetParent(_parent);

        // 비활성화
        go.SetActive(false);

        // 게임 오브젝트에서 PoolingObject 컴포넌트 가져오기
        PoolingObject poolObject = go.GetComponent<PoolingObject>();

        // 오브젝트내에 PoolingObject없을 경우
        if(poolObject == null)
        {
            // PoolingObject 생성
            poolObject = go.AddComponent<PoolingObject>();
        }

        // PoolObject초기화
        poolObject.Init(this);

        // 게임 오브젝트를 스택에 추가
        _pool.Push(go);
    }
    
    /// <summary>
    /// 풀에서 게임오브젝트를 내보내는 함수
    /// </summary>
    public GameObject Pop(Transform parent)
    {
        // 풀에 남은 게임오브젝트가 없는 경우
        if (_pool.Count == 0)
            CreateObject();             // 새 게임오브젝트 생성

        GameObject go = _pool.Pop();
        go.SetActive(true);
        go.transform.SetParent(parent);
        return go;
    }

    /// <summary>
    /// 게임오브젝트를 풀로 되돌리는 함수
    /// </summary>
    /// <param name="go"></param>
    public void Push(GameObject go)
    {
        go.transform.SetParent(_parent);
        go.SetActive(false);
        _pool.Push(go);
    }
}
