using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlotManager : MonoBehaviour
{
    [SerializeField] private Transform _dailyContent;
    [SerializeField] private Transform _weeklyContent;

    private AchievementSlot _dateSlotPrefab;

    private List<AchievementSlot> _dailySlots = new();
    private List<AchievementSlot> _weeklyslots = new();

    [SerializeField] private Button _background;
    [SerializeField] private Toggle _dailyToggle;
    [SerializeField] private Toggle _weeklyToggle;
    [SerializeField] private Toggle _totalToggle;

    private void Awake()
    {
        _background.onClick.AddListener(AchievementPopupClose);

        // 업적 슬롯을 프리팹으로 가지고 있는다.
        _dateSlotPrefab = GameManager.Instance.ResourceManager.GetPrefabOnce<AchievementSlot>("Prefabs/Achievement/DateAchievement.prefab");

        // 데일리 업적 갯수 파악 후 생성한다.
        foreach(AchievementProgressData data in GameManager.Instance.AchievementManager.GetProgressList(AchievementType.Daily))
        {
            // data값에 맞는 설정 데이터 불러오기
            SO_AchievementData achievementData = GameManager.Instance.AchievementManager.GetAchievementData(data.AchievementId);

            // 일일업적 슬롯 생성
            AchievementSlot slot = Instantiate(_dateSlotPrefab, _dailyContent);

            // 일일업적 슬롯 초기화
            slot.Init(achievementData, data);

            // 일일업적 리스트에 추가
            _dailySlots.Add(slot);
        }

        // 위클리 업적 갯수 파악 후 생성한다.
        foreach (AchievementProgressData data in GameManager.Instance.AchievementManager.GetProgressList(AchievementType.Weekly))
        {
            // data값에 맞는 설정 데이터 불러오기
            SO_AchievementData achievementData = GameManager.Instance.AchievementManager.GetAchievementData(data.AchievementId);

            // 주간업적 슬롯 생성
            AchievementSlot slot = Instantiate(_dateSlotPrefab, _weeklyContent);

            // 주간업적 슬롯 초기화
            slot.Init(achievementData, data);

            // 주간업적 리스트에 추가
            _weeklyslots.Add(slot);
        }
    }

    private void AchievementPopupClose()
    {
        gameObject.SetActive(false);
    }
}