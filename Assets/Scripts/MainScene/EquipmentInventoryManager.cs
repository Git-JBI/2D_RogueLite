using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventoryManager : MonoBehaviour
{
    /// <summary>
    /// 장비 아이템 슬롯 프리펩
    /// </summary>
    [SerializeField] private EquipmentItemSlot _equipmentSlot;

    /// <summary>
    /// 장비 아이템 슬롯 리스트
    /// </summary>
    private List<EquipmentItemSlot> _slots = new();

    /// <summary>
    /// 선택된 아이템
    /// </summary>
    [SerializeField]private EquipmentItemData _selectedItem;

    public void Init()
    {
        // 슬롯 리스트 내에 남아있을 경우
        if (_slots.Count != 0)
            DestroySlots();

        // 슬롯 리스트 초기화
        InitSlots();
    }

    /// <summary>
    /// 슬롯 리스트 초기화 함수
    /// </summary>
    private void InitSlots()
    {
        foreach(EquipmentItemData x in GameManager.Instance.GameModel.InventoryItems)
        {
            // 장비 아이템 슬롯 생성(이곳으로 부모 설정)
            EquipmentItemSlot slot = Instantiate(_equipmentSlot, transform);
            // 장비 아이템 슬롯 초기화
            slot.Init(x);
            // 슬롯 이벤트 추가
            slot.OnItemSlotClicked += SetSelectedItem;

            // 슬롯 리스트에 슬롯 추가
            _slots.Add(slot);
        }
    }

    /// <summary>
    /// 슬롯 리스트 비우는 함수
    /// </summary>
    private void DestroySlots()
    {
        foreach(EquipmentItemSlot slot in _slots)
        {
            // 슬롯 리스트 내에 있는 슬롯 파괴
            Destroy(slot);
        }

        // 슬롯 리스트 비우기
        _slots.Clear();
    }

    /// <summary>
    /// 선택된 아이템 설정
    /// </summary>
    /// <param name="item"></param>
    private void SetSelectedItem(EquipmentItemData item)
    {
        _selectedItem = item;
    }

    /// <summary>
    /// 선택된 아이템 비우기
    /// </summary>
    private void EmptySelectedItem()
    {
        _selectedItem = null;
    }

    private void OpenPopup(EquipmentItemData data)
    {

    }
}
