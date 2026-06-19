using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    private HeroController _heroController;
    private Joystick _joystick;
    private GameSceneUIManager _ui;
    private EnemySpawner _enemySpawner;

    private GameSceneModel _gameSceneModel;
    private SO_StageData _stageData;
    
    private int _stage = GameManager.Instance.GameModel.SelectStage;
    private bool _isKillCountReported = false;
    private bool _isBossSpawn = false;

    private void Awake()
    {
        _heroController = FindFirstObjectByType<HeroController>();
        _joystick = FindFirstObjectByType<Joystick>();
        _ui = FindFirstObjectByType<GameSceneUIManager>();
        _enemySpawner = FindFirstObjectByType<EnemySpawner>();

        _gameSceneModel = new GameSceneModel();

        _stageData = Resources.Load<SO_StageData>($"GameDatas/StageData/StageData{_stage}");

        _enemySpawner.OnEnemyDead += EnemyDead;
        _enemySpawner.OnBossDead += BossDead;

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
        if (_gameSceneModel.StageTime <= _stageData.StageTimeLimit & !_isBossSpawn)
        {
            _gameSceneModel.IncreaseStageTime(Time.deltaTime);
            _ui.SetTimeText(_gameSceneModel.StageTime);
        }
        else
        {
            _isBossSpawn = true;

            // 보스 등장
            _enemySpawner.SpawnBoss();
        }
    }

    private void Init()
    {
        _heroController.OnChangeLevel += _ui.SetLevelText;
        _heroController.OnChangeExp += _ui.SetExpBar;
        _heroController.OnUpgradedActive += _ui.SetUpgradePanelActive;
        _heroController.OnDead += PlayerDead;

        _heroController.Init();
        _enemySpawner.Init(_heroController.transform);
    }

    /// <summary>
    /// 스테이지 클리어 시 호출할 함수
    /// </summary>
    private void ClearStage()
    {
        // 최대 스테이지 +1 증가
        GameManager.Instance.GameModel.IncreaseMaxStage();
    }

    /// <summary>
    /// 플레이어 사망 시 호출할 함수
    /// </summary>
    private void PlayerDead()
    {
        ReportKillCount();
        _ui.Fail();
    }

    /// <summary>
    /// 적 사망 시 호출할 함수
    /// </summary>
    private void EnemyDead()
    {
        // 적 사망 수 1 증가
        _gameSceneModel.IncreaseEnemyDeadCount(1);
        
        // 적 사망 텍스트 죽은 숫자로 적용
        _ui.SetDeadCount(_gameSceneModel.EnemyDeadCount);
    }

    /// <summary>
    /// 보스 사망 시 호출할 함수
    /// </summary>
    private void BossDead()
    {
        //TODO 플레이어 경험치 획득, 아이템/장비 획득

        GameManager.Instance.GameModel.AddGold(_gameSceneModel.StageGold);

        // 업적 적용
        ReportKillCount();
        ReportBossKillCount();

        // 시간 흐름 X
        Time.timeScale = 0;

        // 클리어 창 띄우기
        _ui.Clear();
    }

    private void ReportKillCount()
    {
        // killcount가 report되었는지 확인, 적 처치수가 0 이하인 경우는 할 필요가 없음
        if (_isKillCountReported || _gameSceneModel.EnemyDeadCount <= 0)
            return;
        
        _isKillCountReported = true;

        GameManager.Instance.AchievementManager.Report(AchievementConditionType.KillEnemy, _gameSceneModel.EnemyDeadCount);
    }

    private void ReportBossKillCount()
    {
        GameManager.Instance.AchievementManager.Report(AchievementConditionType.KillBoss);
    }
}
