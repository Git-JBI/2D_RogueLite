using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlotViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _achievementName;
    [SerializeField] private TextMeshProUGUI _achievementProgress;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Button _checkButton;

    public void SetName(string name)
    {
        _achievementName.text = name;
    }

    public void SetView(string achievementName, int progress, int target, bool isCompleted, bool isRewardClaimed)
    {
        _achievementName.text = achievementName;
        _achievementProgress.text = $"{Mathf.Min(progress, target)} / {target}";

        if(isRewardClaimed)
        {
            _checkButton.onClick.RemoveAllListeners();
            _buttonText.text = "¿Ï·á";
        }
        else if(isCompleted)
        {
            _buttonText.text = "È¹µæ";
        }
        else
        {
            _buttonText.text = "¹Ì¿Ï·á";
        }
    }
}