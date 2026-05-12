using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    //TODO 추후 Find로 변경
    [SerializeField] private HeroController _heroController;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private UIManager _ui;
    [SerializeField] private EnemySpawner _enemySpawner;

    private GameSceneModel _sceneModel;

    private bool _isBossTime = false;

    private void Awake()
    {
        _sceneModel = GetComponent<GameSceneModel>();

        _enemySpawner.OnEnemyDead += EnemyDead;

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
        if (_sceneModel.StageTime <= 60 & !_isBossTime)
        {
            _sceneModel.IncreaseStageTime(Time.deltaTime);
            _ui.SetTimeText(_sceneModel.StageTime);
        }

        //TODO 추후 보스 등장시간 변경
        
        if(_sceneModel.StageTime > 60)
        {
            _isBossTime = true;
            
            // 보스 등장

            // 보스의 사망이벤트에 여러가지 추가

        }
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

    private void EnemyDead()
    {
        // 적 사망 수 1 증가
        _sceneModel.IncreaseEnemyDeadCount(1);
        
        // 적 사망 텍스트 죽은 숫자로 적용
        _ui.SetDeadCount(_sceneModel.EnemyDeadCount);
    }
}
