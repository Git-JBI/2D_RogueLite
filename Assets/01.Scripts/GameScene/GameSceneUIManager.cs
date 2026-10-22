using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameSceneUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countDeadEnemiesText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Image _expBar;

    private GameFinishViewer _gameFinishUI;
    private WarningEffectViewer _warningEffectViewer;
    private UpgradeViewer _upgradeViewer;

    public IUpgradeViewer IupgradeViewer => _upgradeViewer;

    public event UnityAction OnPauseButtonClicked;

    private void Awake()
    {
        _gameFinishUI = GetComponentInChildren<GameFinishViewer>(true);
        _warningEffectViewer = GetComponentInChildren<WarningEffectViewer>(true);
        _upgradeViewer = GetComponentInChildren<UpgradeViewer>(true);

        _pauseButton.onClick.AddListener(PauseButtonClick);
    }

    /// <summary>
    /// 적 처치 수 텍스트 설정 함수
    /// </summary>
    /// <param name="count">적 처치 수</param>
    public void SetDeadCount(int count)
    {
        _countDeadEnemiesText.text = count.ToString();
    }
    /// <summary>
    /// 레벨 텍스트 설정 함수
    /// </summary>
    /// <param name="level">레벨</param>
    public void SetLevelText(int level)
    {
        _levelText.text = $"Lv. {level}";
    }
    /// <summary>
    /// 시간 텍스트 설정 함수
    /// </summary>
    /// <param name="time">초(Seconds)</param>
    public void SetTimeText(float time)
    {
        int minute = (int)(time / 60);
        int second = (int)(time % 60);

        _timeText.text = $"{minute:D2} : {second:D2}";
    }
    //TODO 능력치 선택 전까지는 경험치 바 가득 참 상태
    public void SetExpBar(float curExp, float maxExp)
    {
        //TODO 부드러운 경험치 바 채워짐으로 변경
        _expBar.fillAmount = curExp / maxExp;
    }
    public void PauseButtonClick()
    {
        OnPauseButtonClicked?.Invoke();
        _pausePanel.SetActive(true);
    }

    public void OnBeforeWaveSpawn()
    {
        _warningEffectViewer.ShowWarning(SpawnWarningType.Wave);
    }
    public void OnBeforeBossSpawn()
    {
        _warningEffectViewer.ShowWarning(SpawnWarningType.Boss);
    }
    public void Fail()
    {
        _gameFinishUI.gameObject.SetActive(true);
        _gameFinishUI.Fail();
    }
    public void Clear()
    {
        _gameFinishUI.gameObject.SetActive(true);
        _gameFinishUI.Clear();
    }
}