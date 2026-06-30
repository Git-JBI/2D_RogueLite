using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    private void Awake()
    {
        _requireCount = 0;

        _mergeSlotPrefab = Resources.Load<EquipmentItemSlot>("Prefabs/Slots/ItemSlot");
        _mergeRule = Resources.Load<SO_MergeRuleData>("GameDatas/MergeRuleData/MergeRuleData");
    }

    private void OnEnable()
    {
        // 장비 리스트 기본 정렬
        SortCanMergeSlots();

        // 슬롯 생성
        CreateAllSlots(_items);
    }

    private void OnDisable()
    {
        if (_selectedSlot.gameObject.activeSelf)
            _selectedSlot.gameObject.SetActive(false);
        if (_upgradedSlot.gameObject.activeSelf)
            _upgradedSlot.gameObject.SetActive(false);
        if (_requireOne.activeSelf)
            _requireOne.gameObject.SetActive(false);
        if (_requireTwo.activeSelf)
            _requireTwo.gameObject.SetActive(false);
    }

    /// <summary>
    /// 보유중인 합성 가능한 모든 장비 아이템 정렬 함수
    /// </summary>
    private void SortCanMergeSlots()
    {
        _items = GameManager.Instance.GameModel.EquipmentModel.EquipmentItems;
        
        _items = _items.
            Where(i => !(i.Rarity == ItemRarity.Legendary && i.RarityLevel == 4)).
            OrderBy(i => i.EquipmentType).
            OrderBy(i => i.ItemID).
            OrderByDescending(i => i.Rarity).
            ToList();
    }
    private void CreateAllSlots(IReadOnlyList<EquipmentItemData> items)
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
            slot.OnItemSlotClicked += InitSelectedItemSlot;
            slot.OnItemSlotClicked += InitUpgradedItemSlot;
            slot.OnItemSlotClicked += SortRequireSlots;
            slot.OnItemSlotClicked += InitSelectedRequireItemSlot;

            // 리스트에 추가
            _slots.Add(slot);
        }
    }
    private void InitSelectedItemSlot(EquipmentItemData data)
    {
        // 선택된 슬롯 활성화
        _selectedSlot.gameObject.SetActive(true);

        // 선택된 슬롯 초기화
        _selectedSlot.Init(data);

        // 선택된 슬롯 이벤트 추가
        _selectedSlot.OnItemSlotClicked += EmptySelectedSlot;
    }
    private void EmptySelectedSlot(EquipmentItemData data)
    {
        // 합성 아이템 목록 갱신
        Refresh();

        // 선택된 아이템 슬롯 비활성화
        _selectedSlot.gameObject.SetActive(false);

        // 업그레이드 예시 아이템 슬롯 비활성화
        _upgradedSlot.gameObject.SetActive(false);

        // 업그레이드 재료 슬롯 비활성화
        switch(_requireCount)
        {
            case 1:
                foreach(EquipmentItemSlot x in _requireItems)
                {
                    if(x.gameObject.activeSelf)
                        x.gameObject.SetActive(false);
                }
                _requireOne.gameObject.SetActive(false);
                break;
            case 2:
                foreach (EquipmentItemSlot x in _requireItems)
                {
                    if (x.gameObject.activeSelf)
                        x.gameObject.SetActive(false);
                }
                _requireTwo.gameObject.SetActive(false);
                break;
        }

        // 업그레이드에 필요 재료 수 초기화
        _requireCount = 0;

        // 업그레이드에 필요 재료 리스트 초기화
        _requireItems.Clear();
    }
    private void InitUpgradedItemSlot(EquipmentItemData data)
    {
        // 업그레이드 예시 슬롯 활성화
        _upgradedSlot.gameObject.SetActive(true);
        // 업그레이드 예시 슬롯 초기화
        _upgradedSlot.Init(GetUpgradedEquipmentData(data));
    }
    private void InitRequireItem(EquipmentItemData data)
    {
        Debug.Log(data.ItemID);
        foreach(EquipmentItemSlot x in _requireItems)
        {
            if(x.ItemData == null)
            {
                // 슬롯 켜기
                x.gameObject.SetActive(true);
                // 선택한 아이템 옮기기
                x.Init(data);
                // 선택한 아이템을 누르면 실행할 함수 이벤트 추가(누르면 선택된 재료 아이템 슬롯은 비워지며 재료 리스트에 추가가 되며 슬롯은 비활성화됨)
                x.OnItemSlotClicked += EmptyRequireItem;

                EquipmentItemSlot selectedSlot = _slots.Find(item => item.ItemData.InstanceID == data.InstanceID);
                // 선택한 아이템 슬롯 리스트에서 버리기
                _slots.Remove(selectedSlot);
                // 선택한 아이템 슬롯 제거
                Destroy(selectedSlot.gameObject);

                return;
            }
        }
    }
    private void EmptyRequireItem(EquipmentItemData data)
    {
        Debug.Log("선택된 재료 아이템 슬롯 비우기");

        // 장비 재료 선택 리스트에 추가
        EquipmentItemSlot slot = Instantiate(_mergeSlotPrefab, _content);
        slot.Init(data);
        slot.OnItemSlotClicked += InitRequireItem;
        _slots.Add(slot);

        EquipmentItemSlot selectedSlot = _requireItems.Find(x => x.ItemData.InstanceID == data.InstanceID);
        selectedSlot.gameObject.SetActive(false);
        selectedSlot.Init(null);
    }
    private void InitSelectedRequireItemSlot(EquipmentItemData data)
    {
        switch (GetRequireCount(data))
        {
            case 1:
                _requireOne.gameObject.SetActive(true);
                _requireCount = GetRequireCount(data); //TODO 값을 여기서 받을게 아니라 업그레이드 선택된 슬롯의 RequireCount를 계속 비교
                _requireItems = _requireOne.gameObject.GetComponentsInChildren<EquipmentItemSlot>(true).ToList();
                break;
            case 2:
                _requireTwo.gameObject.SetActive(true);
                _requireCount = GetRequireCount(data);
                _requireItems = _requireTwo.gameObject.GetComponentsInChildren<EquipmentItemSlot>(true).ToList();
                break;
        }
    }
    private void SortRequireSlots(EquipmentItemData data)
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
        CreateRequireSlots(items);
    }
    private void CreateRequireSlots(List<EquipmentItemData> datas)
    {
        if (_slots.Count != 0)
            DestroySlots();

        foreach (EquipmentItemData x in datas)
        {
            EquipmentItemSlot slot = Instantiate(_mergeSlotPrefab, _content);

            slot.Init(x);

            slot.OnItemSlotClicked += InitRequireItem;

            _slots.Add(slot);
        }
    }

    private void Refresh()
    {
        SortCanMergeSlots();

        CreateAllSlots(_items);
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