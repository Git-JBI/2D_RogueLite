using UnityEngine;
using UnityEngine.Events;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private HeroController _heroController;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameSceneUIManager _uiManager;
    [SerializeField] private EnemySpawner _enemySpawner;

    private GameModel _gameModel;
    private AchievementManager _achievementManager;

    private GameSceneModel _gameSceneModel;
    private SO_StageData _stageData;

    private bool _isKillCountReported;
    private bool _isBossSpawn;
    //TODO bool[]생성 후 밑에 2개는 제거
    private bool _isFirstBurstStarted;
    private bool _isSecondBurstStarted;

    /// <summary>
    /// T0: 웨이브 실행 횟수, T1: 한 웨이브에 스폰될 적의 수, T2: 웨이브 사이의 딜레이
    /// </summary>
    public event UnityAction<int, int, float> OnWaveStarted;
    
    private void Awake()
    {
        _gameModel = GameManager.Instance.GameModel;
        _achievementManager = GameManager.Instance.AchievementManager;

        _gameSceneModel = new GameSceneModel();
        _stageData = GameManager.Instance.ResourceManager.GetScriptable($"GameDatas/StageData/StageData{_gameModel.SelectStage}") as SO_StageData;

        _isKillCountReported = false;
        _isBossSpawn = false;
        _isFirstBurstStarted = false;
        _isSecondBurstStarted = false;
    }

    private void Start()
    {
        _heroController.Init(_gameSceneModel, _uiManager.IupgradeViewer);
        _enemySpawner.Init(_gameModel.SelectStage, _heroController.transform, _stageData, this);

        OnWaveStarted += _enemySpawner.WaveSpawn;

        _enemySpawner.OnMinionDead += EnemyDead;
        _enemySpawner.OnBossDead += BossDead;

        _joystick.OnTouchDown += _heroController.MoveStart;
        _joystick.OnTouchUp += _heroController.MoveFinish;
        _joystick.OnDragging += _heroController.Move;

        _heroController.OnChangeLevel += _uiManager.SetLevelText;
        _heroController.OnChangeExp += _uiManager.SetExpBar;
        _heroController.OnDead += PlayerDead;

        _gameSceneModel.OnIncreaseEnemyDeadCount += _uiManager.SetDeadCount;
    }

    private void Update()
    {
        CheckTime();
    }

    /// <summary>
    /// 스테이지 시간진행 체크
    /// </summary>
    private void CheckTime()
    {
        if (_isBossSpawn)
            return;

        if (_gameSceneModel.StageTime <= _stageData.StageLimitTime)
        {
            _gameSceneModel.IncreaseStageTime(Time.deltaTime);
            _uiManager.SetTimeText(_gameSceneModel.StageTime);
        }
        else
        {
            _isBossSpawn = true;

            //TODO 보스 등장 변경 필요
            _uiManager.OnBeforeBossSpawn();

            // 보스 등장
            _enemySpawner.SpawnStageBoss();
        }

        //TODO 설정데이터에 설정해둔 횟수를 통해 전체 시간에서 나눠서 bool[] 변수 초기화 후 체킹 변수 초기화 시점을 설정데이터 불러오고 난 후로 지정하면 될 듯
        // 첫번째 물량 공세 체크
        if(_gameSceneModel.StageTime >= _stageData.StageLimitTime / 3 && !_isFirstBurstStarted)
        {
            //TODO 물량공세 등장 변경 필요
            _uiManager.OnBeforeWaveSpawn();

            _isFirstBurstStarted = true;
            // 물량 공세 이벤트 발행
            OnWaveStarted?.Invoke(3, 40, 3.0f);
        }
        // 두번째 물량 공세 체크
        if(_gameSceneModel.StageTime >= _stageData.StageLimitTime / 3 * 2 && !_isSecondBurstStarted)
        {
            _uiManager.OnBeforeWaveSpawn();
            _isSecondBurstStarted = true;
            // 물량 공세 이벤트 발행
            OnWaveStarted?.Invoke(3, 40, 3.0f);
        }
    }

    /// <summary>
    /// 스테이지 클리어 시 호출할 함수
    /// </summary>
    private void ClearStage()
    {
        Debug.Log("스테이지 클리어");

        // 최대 진행가능 스테이지 == 현재 진행중인 스테이지 일 경우
        if (_gameModel.MaxStage == _gameModel.SelectStage)
        {
            Debug.Log("최대 스테이지 +1");
            // 최대 스테이지 +1 증가
            _gameModel.IncreaseMaxStage();
            // 선택한 스테이지 설정
            _gameModel.SetSelectStage(_gameModel.MaxStage);
            // 저장
            _gameModel.SaveGameData();
        }
    }

    /// <summary>
    /// 플레이어 사망 시 호출할 함수
    /// </summary>
    private void PlayerDead()
    {
        ReportKillCount();
        _uiManager.Fail();
    }

    /// <summary>
    /// 적 사망 시 호출할 함수
    /// </summary>
    private void EnemyDead()
    {
        // 적 사망
        _gameSceneModel.IncreaseEnemyDeadCount();
    }

    /// <summary>
    /// 보스 사망 시 호출할 함수
    /// </summary>
    private void BossDead()
    {
        //TODO 플레이어 경험치 획득, 아이템/장비 획득

        _gameModel.AddGold(_gameSceneModel.StageGold);

        // 업적 적용
        ReportKillCount();
        ReportBossKillCount();

        // 시간 흐름 X
        Time.timeScale = 0;

        // 클리어 창 띄우기
        _uiManager.Clear();

        // 스테이지 클리어
        ClearStage();
    }

    private void ReportKillCount()
    {
        // killcount가 report되었는지 확인, 적 처치수가 0 이하인 경우는 할 필요가 없음
        if (_isKillCountReported || _gameSceneModel.EnemyDeadCount <= 0)
            return;
        
        _isKillCountReported = true;

        _achievementManager.Report(AchievementConditionType.KillEnemy, _gameSceneModel.EnemyDeadCount);
    }

    private void ReportBossKillCount()
    {
        _achievementManager.Report(AchievementConditionType.KillBoss);
    }

    public float GetTime()
    {
        return _gameSceneModel.StageTime;
    }
}