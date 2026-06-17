using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentInventoryManager : MonoBehaviour
{
    /// <summary>
    /// 장비 아이템 슬롯 프리펩
    /// </summary>
    [SerializeField] private EquipmentItemSlot _equipmentSlot;

    /// <summary>
    /// 장비 아이템 세부내용을 보여줄 팝업
    /// </summary>
    [SerializeField] private EquipmentItemDetailPopup _popup;
    /// <summary>
    /// 장비 아이템 슬롯 리스트
    /// </summary>
    private List<EquipmentItemSlot> _slots = new();

    private void Awake()
    {
        _popup.OnPopupClosed += Init;
    }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Init()
    {
        Debug.Log("장비 인벤토리 초기화");
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
        // 리스트 복제본을 생성 후 1. 장비 아이템 타입에 따라 정렬 2. 타입에 따른 종류에 따라 정렬 3. 레어리티에 따라 정렬
        var items = GameManager.Instance.GameModel.EquipmentModel.EquipmentItems.OrderBy(i => i.EquipmentType).OrderBy(i => i.ItemID).OrderByDescending(i => i.Rarity).ToList();

        foreach (EquipmentItemData x in items)
        {
            // 장비 아이템 슬롯 생성(이곳으로 부모 설정)
            EquipmentItemSlot slot = Instantiate(_equipmentSlot, transform);
            // 장비 아이템 슬롯 초기화
            slot.Init(x);
            // 슬롯 이벤트 추가
            slot.OnItemSlotClicked += OpenPopup;
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
            Destroy(slot.gameObject);
        }

        // 슬롯 리스트 비우기
        _slots.Clear();
    }

    private void OpenPopup(EquipmentItemData data)
    {
        _popup.gameObject.SetActive(true);
        _popup.Init(data);
        _popup.SetEquip();
    }
}
