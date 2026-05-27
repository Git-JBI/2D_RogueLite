using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EquipmentItemSlot : MonoBehaviour, IPointerClickHandler
{
    // 아이템 슬롯을 보여줄 뷰어
    private EquipmentItemViewer _itemViewer;

    // 슬롯이 가지고 있을 아이템 데이터
    private EquipmentItemData _itemData;

    public event UnityAction<EquipmentItemData> OnItemSlotClicked;

    private void Awake()
    {
        _itemViewer = GetComponent<EquipmentItemViewer>();
    }

    public void Init(EquipmentItemData data)
    {
        _itemData = data;

        string desc = GameManager.Instance.EquipmentItemDatabase.GetEquipmentItemDesc(data.ItemID);
        _itemViewer.Init(data.Rarity, desc);
    }

    /// <summary>
    /// 버튼을 눌렀을 시 실행되는 함수
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        OnItemSlotClicked?.Invoke(_itemData);
    }
}
