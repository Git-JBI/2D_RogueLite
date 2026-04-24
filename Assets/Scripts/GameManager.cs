using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    private PoolManager _poolManager;
    private ResourceManager _resourceManager;

    public PoolManager PoolManager => _poolManager;
    public ResourceManager ResourceManager => _resourceManager;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

            return;
        }

        _poolManager = GetComponent<PoolManager>();
        _resourceManager = GetComponent<ResourceManager>();
    }
}
