using UnityEngine;

public class PoolingObject : MonoBehaviour
{
    /// <summary>
    /// 자신이 속한 풀
    /// </summary>
    private Pool _pool;

    /// <summary>
    /// 초기화 함수
    /// </summary>
    /// <param name="pool"></param>
    public void Init(Pool pool)
    {
        _pool = pool;
    }

    /// <summary>
    /// 이 오브젝트를 풀로 되돌리는 함수
    /// </summary>
    public void ReturnToPool()
    {
        _pool.Push(gameObject);
    }
}
