using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private TextMeshProUGUI _countDeadEnemiesText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Image _expBar;

    public void IncreaseDeadCount(int count)
    {
        _countDeadEnemiesText.text = count.ToString();
    }

    public void SetLevelText(int level)
    {
        _levelText.text = $"Lv. {level}";
    }

    public void SetExpBar(float curExp, float maxExp)
    {
        //TODO 부드러운 경험치 바 채워짐
        _expBar.fillAmount = curExp / maxExp;
    }

    public void SetUpgradePanelActive(bool isActive)
    {
        _upgradePanel.SetActive(isActive);
    }
}
