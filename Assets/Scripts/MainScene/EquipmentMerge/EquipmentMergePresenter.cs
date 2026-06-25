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
        _mergeSlotPrefab = Resources.Load<EquipmentItemSlot>("Prefabs/Slots/ItemSlot");
        _mergeRule = Resources.Load<SO_MergeRuleData>("GameDatas/MergeRuleData/MergeRuleData");
        _items = GameManager.Instance.GameModel.EquipmentModel.EquipmentItems;

        _mergeState = MergeState.None;
    }

    // 합성 창 입장 시 장비아이템 리스트 표시(아이템을 누르면 "_selectedSlot"의 활성화와 그 아이템이 띄워지도록, 아이템 슬롯 리스트 필터 정렬)
    // 
    private void OnEnable()
    {
        // 장비 리스트 기본 정렬
        InitSlots();

        // 슬롯 생성
        CreateSlots(_items);
    }

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

    public void CreateFilterSlots(EquipmentType equipmentType, ItemRarity itemRarity)
    {
        List<EquipmentItemData> items = _items.Where(i => i.Rarity == itemRarity && i.EquipmentType == equipmentType).ToList();
        CreateSlots(items);
    }
    public void CreateFilterSlots(EquipmentType equipmentType, ItemRarity itemRarity, string itemId)
    {
        List<EquipmentItemData> items = _items.Where(i => i.EquipmentType == equipmentType && i.Rarity == itemRarity && i.ItemID == itemId).ToList();
    }

    private void SetItemSlot(EquipmentItemData data)
    {
        switch(_mergeState)
        {
            case MergeState.None:
                SelectBaseItem(data);
                CreateFilterSlots(data.EquipmentType, data.Rarity);
                break;
            case MergeState.BaseSelected:
                break;
            case MergeState.RequireSelected:
                break;
        }

        
    }

    private void SelectBaseItem(EquipmentItemData data)
    {
        // 선택된 슬롯 활성화
        _selectedSlot.gameObject.SetActive(true);
        // 선택된 슬롯 초기화
        _selectedSlot.Init(data);
        // 선택된 슬롯 이벤트 추가
        _selectedSlot.OnItemSlotClicked += EmptySelectedSlot;
    }
    private void SelectRequireItem(EquipmentItemData data)
    {

    }

    private void EmptySelectedSlot(EquipmentItemData data)
    {
        Refresh();
        _selectedSlot.gameObject.SetActive(false);
    }

    private void Refresh()
    {
        CreateSlots(_items);
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