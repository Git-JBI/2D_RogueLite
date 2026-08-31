using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlotManager : MonoBehaviour
{
    [SerializeField] private Transform _dailyContent;
    [SerializeField] private Transform _weeklyContent;

    private AchievementManager _achievementManager;
    private AchievementModel _achievementModel;
    private AchievementSlot _dateSlotPrefab;

    private List<AchievementSlot> _dailySlots = new();
    private List<AchievementSlot> _weeklySlots = new();

    [SerializeField] private Button _background;
    [SerializeField] private Toggle _dailyToggle;
    [SerializeField] private Toggle _weeklyToggle;
    [SerializeField] private Toggle _totalToggle;

    private void Awake()
    {
        _achievementManager = GameManager.Instance.AchievementManager;
        _achievementModel = GameManager.Instance.GameModel.AchievementModel;

        _background.onClick.AddListener(AchievementPopupClose);

        // 업적 슬롯을 프리팹으로 가지고 있는다.
        _dateSlotPrefab = Resources.Load<AchievementSlot>("Prefabs/Achievement/DateAchievement");

        //TODO 업적 진행 완료 > 미완료 > 보상 받음 순서로 업적슬롯리스트 갱신

        // 일일 업적 리스트 생성
        CreateSlots(AchievementType.Daily, _dailyContent, _dailySlots);

        // 주간 업적 리스트 생성
        CreateSlots(AchievementType.Weekly, _weeklyContent, _weeklySlots);
    }

    /// <summary>
    /// 업적 타입에 따른 업적 슬롯 생성 기능
    /// </summary>
    /// <param name="type"></param>
    /// <param name="content"></param>
    /// <param name="slotList"></param>
    private void CreateSlots(AchievementType type, Transform content, List<AchievementSlot> slotList)
    {
        foreach (AchievementProgressData progressData in _achievementModel.Achievements)
        {
            SO_AchievementData achievementData = _achievementManager.GetAchievementData(progressData.AchievementId);

            if (achievementData == null)
                continue;

            if (achievementData.Type != type)
                continue;

            AchievementSlot slot = Instantiate(_dateSlotPrefab, content);
            slot.Init(achievementData, progressData);

            slotList.Add(slot);
        }
    }

    private void AchievementPopupClose()
    {
        gameObject.SetActive(false);
    }
}