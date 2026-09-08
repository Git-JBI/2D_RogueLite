using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentInventoryManager : MonoBehaviour
{
    private EquipmentItemDatabase _itemBase;

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
        _itemBase = GameManager.Instance.EquipmentItemDatabase;

        _popup.OnPopupClosed += Init;
    }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Init()
    {
        // 슬롯 리스트 초기화
        InitSlots();
    }

    /// <summary>
    /// 슬롯 리스트 초기화 함수
    /// </summary>
    private void InitSlots()
    {
        // 리스트 복제본을 생성 후 1. 장비 아이템 타입에 따라 정렬 2. 타입에 따른 종류에 따라 정렬 3. 레어리티에 따라 정렬
        var items = GameManager.Instance.GameModel.EquipmentModel.EquipmentItems.
            OrderByDescending(i => i.Rarity).
            ThenBy(i => i.EquipmentType).
            ThenByDescending(i => i.RarityLevel).
            ThenBy(i => i.ItemID).
            ToList();

        for (int i = 0; i < items.Count; i++)
        {
            EquipmentItemSlot slot;

            if (i < _slots.Count)
            {
                slot = _slots[i];
                slot.gameObject.SetActive(true);
            }
            else
            {
                GameObject slotObject = GameManager.Instance.PoolManager.GetFromPool("Prefabs/Slots/ItemSlot", transform);

                slot = slotObject.GetComponent<EquipmentItemSlot>();
                _slots.Add(slot);
            }
            EquipmentItemData data = items[i];
            string itemId = data.ItemID;

            SO_EquipmentItemData _itemConfigData = _itemBase.GetEquipmentItemData(itemId);
            Sprite raritySprite = _itemBase.GetRaritySprite(data.Rarity);

            slot.Init(items[i].InstanceID, _itemBase.GetEquipmentItemSprite(itemId), raritySprite, data.ItemLevel, data.RarityLevel);
            slot.OnItemSlotClicked += OpenPopup;
        }
    }

    private void OpenPopup(EquipmentItemData data)
    {
        _popup.gameObject.SetActive(true);
        _popup.Init(data);
        _popup.SetEquipmentItem();
    }
}
