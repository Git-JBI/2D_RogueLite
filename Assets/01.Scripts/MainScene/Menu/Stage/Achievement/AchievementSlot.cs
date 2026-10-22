using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
    [SerializeField] private Button _button;

    private AchievementProgressData _progressData;
    private SO_AchievementData _achievementData;
    private AchievementSlotViewer _viewer;

    private void Awake()
    {
        _viewer = GetComponent<AchievementSlotViewer>();
    }

    public void Init(SO_AchievementData achievementData, AchievementProgressData progressData)
    {
        _achievementData = achievementData;
        _progressData = progressData;

        Refresh();

        Button button = _viewer.GetButton();
        button.onClick.AddListener(CheckAchievement);
    }

    public void Refresh()
    {
        _viewer.SetView(_achievementData.AchievementName, _progressData.Progress, _achievementData.TargetValue, _progressData.IsCompleted, _progressData.IsRewardClaimed);
    }

    /// <summary>
    /// 보상을 받을 수 있는지 판단
    /// </summary>
    private void CheckAchievement()
    {
        // 이미 보상을 받은 경우
        if (_progressData.IsRewardClaimed)
            return;

        // 안받은 경우
        else
        {
            // 업적 목표를 달성했을 경우
            if (_progressData.IsCompleted)
            {
                // 보상 획득
                GameManager.Instance.AchievementManager.GetAchievementReward(_progressData);
                // 리프레시
                Refresh();
            }

            // 업적 목표를 달성하지 못했을 경우
            else
                return;
        }
    }
}
