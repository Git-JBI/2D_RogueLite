using TMPro;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    //TODO 추후 Find로 변경
    [SerializeField] private HeroController _heroController;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameSceneUIManager _ui;
    [SerializeField] private EnemySpawner _enemySpawner;

    private GameSceneModel _sceneModel;

    private bool _isBossTime = false;
    private bool _isBossSpawn = false;

    private void Awake()
    {
        _sceneModel = GetComponent<GameSceneModel>();

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
        if (_sceneModel.StageTime <= 60 & !_isBossTime)
        {
            _sceneModel.IncreaseStageTime(Time.deltaTime);
            _ui.SetTimeText(_sceneModel.StageTime);
        }

        //TODO 추후 보스 등장시간 스테이지마다 다르도록 변경
        
        if(_sceneModel.StageTime > 60 & !_isBossSpawn)
        {
            _isBossTime = true;
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
        _heroController.OnDead += _ui.Fail;

        _heroController.Init();
    }

    /// <summary>
    /// 적 사망 시 호출할 함수
    /// </summary>
    private void EnemyDead()
    {
        // 적 사망 수 1 증가
        _sceneModel.IncreaseEnemyDeadCount(1);
        
        // 적 사망 텍스트 죽은 숫자로 적용
        _ui.SetDeadCount(_sceneModel.EnemyDeadCount);
    }

    /// <summary>
    /// 보스 사망 시 호출할 함수
    /// </summary>
    private void BossDead()
    {
        //TODO 플레이어 경험치 획득, 아이템/장비 획득

        //TODO 스테이지 내에서 획득한 골드 추가
        // GameManager.Instance.GameData.AddGold(_sceneModel.StageGold);

        // 시간 흐름 X
        Time.timeScale = 0;

        // 클리어 창 띄우기
        _ui.Clear();
    }
}
