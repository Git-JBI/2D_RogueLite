using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum MergeState
{
    None,
    BaseSelected,
    RequireSelected,
}

public class EquipmentMergePresenter : MonoBehaviour
{
    /* Common + Commom + Common = UnCommon (동일 아이템ID)
    // UnCommon + UnCommon + UnCommon = Rare (동일 아이템ID)
    // Rare + Rare + Rare = Unique (동일 아이템ID)
    // Unique + Unique = Unique+1 (동일 아이템 타입)
    // Unique+1 + Unique + Unique = Unique+2 (동일 아이템 타입)
    // Unique+2 + Unique+2 = Epic (동일 아이템ID, 동일 등급, 동일 레어리티 레벨)
    // Epic + Epic = Epic+1 (동일 아이템 타입)
    // Epic+1 + Epic = Epic+2 (동일 아이템 타입)
    // Epic+2 + Epic = Epic+3 (동일 아이템 타입)
    // Epic+3 + Epic + Epic = Legendary (동일 아이템ID, 동일 등급, 레어리티 레벨 0)
    // Legendary + Legendary = Legendary+1 (동일 아이템 타입)
    // Legendary+1 + Legendary = Legendary+2 (동일 아이템 타입)
    // Legendary+2 + Legendary = Legendary+3 (동일 아이템 타입)
    // Legendary+3 + Legendary = Legendary+4 (동일 아이템 타입)*/
    [Header("# 선택된 아이템 슬롯")]
    [SerializeField] private EquipmentItemSlot _selectedSlot;

    [Header("# 업그레이드 결과 아이템 슬롯")]
    [SerializeField] private EquipmentItemSlot _upgradedSlot;

    [Header("# 장비 아이템을 생성할 부모 Transform")]
    [SerializeField] private Transform _content;

    [Header("# 재료가 한 개 필요할 시 쓰는 GameObject")]
    [SerializeField] private GameObject _requireOne;

    [Header("# 재료가 두 개 필요할 시 쓰는 GameObject")]
    [SerializeField] private GameObject _requireTwo;

    private int _requireCount;
    private List<EquipmentItemSlot> _requireItems = new();

    /// <summary>
    /// 생성해 줄 아이템 슬롯
    /// </summary>
    private EquipmentItemSlot _mergeSlotPrefab;
    /// <summary>
    /// 장비 합성 규칙 설정 데이터
    /// </summary>
    private SO_MergeRuleData _mergeRule;
    /// <summary>
    /// 가지고 있는 장비 아이템 리스트
    /// </summary>
    private IReadOnlyList<EquipmentItemData> _items;
    
    private List<EquipmentItemSlot> _slots = new();

    private MergeState _mergeState;

    private void Awake()
    {
        _requireCount = 0;

        _mergeSlotPrefab = Resources.Load<EquipmentItemSlot>("Prefabs/Slots/ItemSlot");
        _mergeRule = Resources.Load<SO_MergeRuleData>("GameDatas/MergeRuleData/MergeRuleData");

        _mergeState = MergeState.None;
    }

    // 합성 창 입장 시 장비아이템 리스트 표시(아이템을 누르면 "_selectedSlot"의 활성화와 그 아이템이 띄워지도록, 아이템 슬롯 리스트 필터 정렬)
    
    private void OnEnable()
    {
        // 장비 리스트 설정
        _items = GameManager.Instance.GameModel.EquipmentModel.EquipmentItems;

        // 장비 리스트 기본 정렬
        InitSlots();

        // 슬롯 생성
        CreateSlots(_items);
    }

    private void OnDisable()
    {
        if(_selectedSlot.gameObject.activeSelf)
            _selectedSlot.gameObject.SetActive(false);
        if(_upgradedSlot.gameObject.activeSelf)
            _upgradedSlot.gameObject.SetActive(false);
        if(_requireOne.activeSelf)
            _requireOne.gameObject.SetActive(false);
        if(_requireTwo.activeSelf)
            _requireTwo.gameObject.SetActive(false);
    }

    // 뒤로 가기를 눌렀을 때 모든 설정된것들 비활성화
    /// <summary>
    /// 장비 아이템 기본 정렬 함수
    /// </summary>
    private void InitSlots()
    {
        _items = _items.
            Where(i => !(i.Rarity == ItemRarity.Legendary && i.RarityLevel == 4)).
            OrderBy(i => i.EquipmentType).
            OrderBy(i => i.ItemID).
            OrderByDescending(i => i.Rarity).
            ToList();
    }

    private void CreateSlots(IReadOnlyList<EquipmentItemData> items)
    {
        if (_slots.Count != 0)
            DestroySlots();

        foreach (EquipmentItemData item in items)
        {
            // 머지 아이템 슬롯 생성
            EquipmentItemSlot slot = Instantiate(_mergeSlotPrefab, _content);

            // 초기화
            slot.Init(item);

            // 슬롯 클릭 시 이벤트 추가
            slot.OnItemSlotClicked += SetItemSlot;

            // 리스트에 추가
            _slots.Add(slot);
        }
    }

    public void CreateFilterSlots(EquipmentItemData data)
    {
        List<EquipmentItemData> items = new();
        MergeRule rule = _mergeRule.GetMergeRule(data.Rarity, data.RarityLevel);

        // 업그레이드 후 레어리티가 같을 경우 재료는 동일 레어리티 & 동일 아이템 타입
        if(data.Rarity == rule.UpgradedRarity)
            items = _items.Where(i => i.Rarity == data.Rarity && i.EquipmentType == data.EquipmentType).ToList();
        // 업그레이드 후 레어리티가 다를 경우 재료는 동일 레어리티 & 동일 아이템 타입 & 동일 아이템 ID
        else
            items = _items.Where(i => i.Rarity == data.Rarity && i.EquipmentType == data.EquipmentType && i.ItemID == data.ItemID).ToList();

        // 선택한 아이템은 목록에서 제거
        items.Remove(data);

        // 슬롯 생성
        CreateSlots(items);
    }

    private void SetItemSlot(EquipmentItemData data)
    {
        switch(_mergeState)
        {
            case MergeState.None:
                SelectBaseItem(data);
                CreateFilterSlots(data);
                break;
            case MergeState.BaseSelected:
                Debug.Log("재료 아이템 선택");
                break;
        }
    }

    private void SelectBaseItem(EquipmentItemData data)
    {
        // 베이스 아이템 설정 상태
        _mergeState = MergeState.BaseSelected;
        // 선택된 슬롯 활성화
        _selectedSlot.gameObject.SetActive(true);
        // 선택된 슬롯 초기화
        _selectedSlot.Init(data);
        // 선택된 슬롯 이벤트 추가
        _selectedSlot.OnItemSlotClicked += EmptySelectedSlot;

        // 업그레이드 예시 슬롯 활성화
        _upgradedSlot.gameObject.SetActive(true);
        // 업그레이드 예시 슬롯 초기화
        _upgradedSlot.Init(GetUpgradedEquipmentData(data));
        
        // 필요한 재료의 갯수 나타냄
        switch(GetRequireCount(data))
        {
            case 1:
                _requireOne.gameObject.SetActive(true);
                _requireCount = GetRequireCount(data);
                _requireItems = _requireOne.gameObject.GetComponentsInChildren<EquipmentItemSlot>().ToList();
                break;
            case 2:
                _requireTwo.gameObject.SetActive(true);
                _requireCount = GetRequireCount(data);
                _requireItems = _requireTwo.gameObject.GetComponentsInChildren<EquipmentItemSlot>().ToList();
                break;
        }
    }

    private void SelectRequireItem(EquipmentItemData data)
    {
        if (_requireCount == GetRequireCount(_selectedSlot.ItemData))
            return;
    }

    private void EmptySelectedSlot(EquipmentItemData data)
    {
        // 상태 없음 설정
        _mergeState = MergeState.None;

        // 합성 아이템 목록 갱신
        Refresh();

        // 선택된 아이템 슬롯 비활성화
        _selectedSlot.gameObject.SetActive(false);

        // 업그레이드 예시 아이템 슬롯 비활성화
        _upgradedSlot.gameObject.SetActive(false);

        // 업그레이드 재료 슬롯 비활성화
        if(_requireOne.activeSelf)
            _requireOne.SetActive(false);
        if(_requireTwo.activeSelf)
            _requireTwo.SetActive(false);

        // 업그레이드에 필요 재료 수 초기화
        _requireCount = 0;

        // 업그레이드에 필요 재료 리스트 초기화
        _requireItems.Clear();
    }

    private void Refresh()
    {
        CreateSlots(_items);
    }

    private EquipmentItemData GetUpgradedEquipmentData(EquipmentItemData data)
    {
        MergeRule mergeRule = _mergeRule.GetMergeRule(data.Rarity, data.RarityLevel);
        EquipmentItemData returnData = new("", data.ItemID, data.EquipmentType, mergeRule.UpgradedRarity, mergeRule.UpgradedRarityLevel, data.ItemLevel);

        return returnData;
    }

    private int GetRequireCount(EquipmentItemData data)
    {
        return _mergeRule.GetMergeRule(data.Rarity, data.RarityLevel).RequireCount;
    }

    private void DestroySlots()
    {
        foreach(EquipmentItemSlot slot in _slots)
        {
            // 슬롯 리스트 내의 슬롯 파괴
            Destroy(slot.gameObject);
        }

        // 슬롯 리스트 비우기
        _slots.Clear();
    }
}