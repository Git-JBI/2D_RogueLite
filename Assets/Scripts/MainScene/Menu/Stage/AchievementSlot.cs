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
    }

    public void Refresh()
    {
        //_viewer.SetView();
    }

    private void CheckAchievement()
    {
        // 이미 보상을 받은 경우
        if (_progressData.IsRewardClaimed)
            return;

        // 안받은 경우
        else
        {

        }
    }
}
