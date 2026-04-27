using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countDeadEnemiesText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Image _expBar;

    private void Awake()
    {
        //TODO 추후 수정 : 위치 다른곳으로
        SetLevelText(1);
        SetExpBar(0);
    }

    public void IncreaseDeadCount(int count)
    {
        _countDeadEnemiesText.text = count.ToString();
    }

    public void SetLevelText(int level)
    {
        _levelText.text = $"Lv. {level}";
    }

    public void SetExpBar(float amount)
    {
        _expBar.fillAmount = amount;
    }
}
