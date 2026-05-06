using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    private GameData _gameData;
    private PoolManager _poolManager;
    private ResourceManager _resourceManager;

    public GameData GameData => _gameData;
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

        _gameData = new GameData();
        _poolManager = GetComponent<PoolManager>();
        _resourceManager = GetComponent<ResourceManager>();
    }
}
