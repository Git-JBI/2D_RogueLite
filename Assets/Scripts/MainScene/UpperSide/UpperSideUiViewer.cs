using TMPro;
using UnityEngine;

public class UpperSideUiViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _energyText;
    [SerializeField] private TextMeshProUGUI _emeraldText;
    [SerializeField] private TextMeshProUGUI _goldText;

    public void SetTimeText(int time)
    {
        if (time == 0)
            _timeText.gameObject.SetActive(false);
        else
        {
            int remainTime = time % 600;
            _timeText.text = $"{remainTime / 60:D2} : {remainTime % 60:D2}";
        }
    }
    public void SetEnergyText(int energy, int maxEnergy)
    {
        string text = $"{energy} / {maxEnergy}";
        _energyText.text = text;
    }
    public void SetEmeraldText(int value)
    {
        _emeraldText.text = value.ToAbbreviatedString();
    }
    public void SetGoldText(int value)
    {
        _goldText.text = value.ToAbbreviatedString();
    }
}
