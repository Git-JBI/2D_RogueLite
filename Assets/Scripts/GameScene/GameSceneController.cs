using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    //TODO 추후 Find로 변경
    [SerializeField] private HeroController _heroController;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private UIManager _ui;
    [SerializeField] private EnemySpawner _enemySpawner;
    private GameSceneModel _sceneModel;

    private void Awake()
    {
        _sceneModel = GetComponent<GameSceneModel>();

        _joystick.OnTouchDown += _heroController.MoveStart;
        _joystick.OnTouchUp += _heroController.MoveFinish;
        _joystick.OnDragging += _heroController.Move;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _sceneModel.IncreaseStageTime(Time.deltaTime);
        _ui.SetTimeText(_sceneModel.StageTime);
    }

    private void Init()
    {
        _heroController.OnChangeLevel += _ui.SetLevelText;
        _heroController.OnChangeExp += _ui.SetExpBar;
        _heroController.OnUpgradedActive += _ui.SetUpgradePanelActive;
        _heroController.OnDead += _ui.SetGameOverPanelActive;

        _heroController.Init();
        _enemySpawner.Init(_ui);
    }
}
