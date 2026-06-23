using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentMergeManager : MonoBehaviour
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
    [Header("# 업그레이드 될 시 아이템 슬롯")]
    private EquipmentItemSlot _mergeSlotPrefab;
    private EquipmentModel _equipmentModel;

    private List<EquipmentItemSlot> _slots = new();

    private void Awake()
    {
        _mergeSlotPrefab = Resources.Load<EquipmentItemSlot>("Prefabs/Slots/ItemSlot");

        _equipmentModel = GameManager.Instance.GameModel.EquipmentModel;
    }

    private void OnEnable()
    {
        // 슬롯 내용물이 남아있을 경우 전부 파괴
        if (_slots.Count != 0)
            DestroySlots();

        // 슬롯 생성
        InitSlots();
    }

    /// <summary>
    /// 슬롯 리스트 생성 함수
    /// </summary>
    private void InitSlots()
    {
        IReadOnlyList<EquipmentItemData> items = _equipmentModel.EquipmentItems.OrderBy(i => i.EquipmentType).OrderBy(i => i.ItemID).OrderByDescending(i => i.Rarity).ToList();

        foreach(EquipmentItemData item in items)
        {
            // 레전더리 +4일 경우 리턴
            if (item.Rarity == ItemRarity.Legendary && item.RarityLevel == 4)
                continue;

            // 머지 아이템 슬롯 생성
            EquipmentItemSlot slot = Instantiate(_mergeSlotPrefab, transform);

            // 초기화
            slot.Init(item);

            // 리스트에 추가
            _slots.Add(slot);
        }
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
