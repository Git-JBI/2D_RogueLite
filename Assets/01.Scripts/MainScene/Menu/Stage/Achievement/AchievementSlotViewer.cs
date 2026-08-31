using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlotViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _achievementName;
    [SerializeField] private Image _progressBar;
    [SerializeField] private TextMeshProUGUI _achievementProgress;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Button _checkButton;
    [SerializeField] private Image _checkImage;

    public Button GetButton()
    {
        return _checkButton;
    }

    public void SetName(string name)
    {
        _achievementName.text = name;
    }

    public void SetView(string achievementName, int progress, int target, bool isCompleted, bool isRewardClaimed)
    {
        _achievementName.text = achievementName;
        _progressBar.fillAmount = (float)progress / target;
        _achievementProgress.text = $"{Mathf.Min(progress, target)} / {target}";

        if(isRewardClaimed)
        {
            if (_checkButton.gameObject.activeSelf)
                _checkButton.gameObject.SetActive(false);
            if (!_checkImage.gameObject.activeSelf)
                _checkImage.gameObject.SetActive(true);
        }
        else if(isCompleted && !isRewardClaimed)
        {
            _buttonText.text = "획득";
        }
        else
        {
            _buttonText.text = "미완료";
        }
    }
}