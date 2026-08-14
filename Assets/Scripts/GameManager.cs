using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    private GameModel _gameModel;
    private PoolManager _poolManager;
    private ResourceManager _resourceManager;
    private AchievementManager _achievementManager;
    private EquipmentManager _equipmentManager;
    private EnergyManager _energyManager;
    private EquipmentItemDatabase _equipmentItemDatabase;

    public GameModel GameModel => _gameModel;
    public PoolManager PoolManager => _poolManager;
    public ResourceManager ResourceManager => _resourceManager;
    public AchievementManager AchievementManager => _achievementManager;
    public EquipmentManager EquipmentManager => _equipmentManager;
    public EnergyManager EnergyManager => _energyManager;
    public EquipmentItemDatabase EquipmentItemDatabase => _equipmentItemDatabase;

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
        _achievementManager = GetComponent<AchievementManager>();
        _equipmentManager = GetComponent<EquipmentManager>();
        _energyManager = new EnergyManager(_gameModel.EnergyModel);
        _equipmentItemDatabase = GetComponent<EquipmentItemDatabase>();
    }

    private void Start()
    {
        _achievementManager.Init(_gameModel);
        _equipmentManager.Init(_gameModel);
    }
}
