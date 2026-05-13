using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private TextMeshProUGUI _countDeadEnemiesText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Image _expBar;

    private UI_GameFinish _gameFinishPanel;

    private void Awake()
    {
        _gameFinishPanel = GetComponentInChildren<UI_GameFinish>(true);
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

    public void SetUpgradePanelActive(bool isActive)
    {
        _upgradePanel.SetActive(isActive);
    }

    public void Fail()
    {
        _gameFinishPanel.gameObject.SetActive(true);
        _gameFinishPanel.Fail();
    }

    public void Clear()
    {
        _gameFinishPanel.gameObject.SetActive(true);
        _gameFinishPanel.Clear();
    }
}