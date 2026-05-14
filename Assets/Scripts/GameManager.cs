using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    private GameModel _gameModel;
    private PoolManager _poolManager;
    private ResourceManager _resourceManager;

    public GameModel GameModel => _gameModel;
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

        _gameModel = GetComponent<GameModel>();
        _poolManager = GetComponent<PoolManager>();
        _resourceManager = GetComponent<ResourceManager>();
    }
}
