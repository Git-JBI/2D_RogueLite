using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private GameObject _gameoverPanel;
    [SerializeField] private TextMeshProUGUI _countDeadEnemiesText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Image _expBar;

    public void IncreaseDeadCount(int count)
    {
        _countDeadEnemiesText.text = count.ToString();
    }

    public void SetLevelText(int level)
    {
        _levelText.text = $"Lv. {level}";
    }

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

    public void SetGameOverPanelActive()
    {
        _gameoverPanel.SetActive(true);
    }
}