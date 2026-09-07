using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EquipmentItemSlot : MonoBehaviour, IPointerClickHandler
{
    private string _instanceId;

    /// <summary>
    /// 아이템 슬롯을 보여줄 뷰어
    /// </summary>
    private EquipmentItemViewer _itemViewer;
    /// <summary>
    /// 슬롯이 가지고 있을 아이템 데이터
    /// </summary>
    private EquipmentItemData _itemData;

    public EquipmentItemData ItemData => _itemData;
    /// <summary>
    /// 슬롯이 클릭될 시 실행될 이벤트
    /// </summary>
    public event UnityAction<EquipmentItemData> OnItemSlotClicked;

    public void Init(string instanceId, Sprite itemImage, Sprite rarityImage, int level, int rarityLevel)
    {
        if(!_itemViewer)
            _itemViewer = GetComponent<EquipmentItemViewer>();

        OnItemSlotClicked = null;

        _instanceId = instanceId;

        if(_itemData != null)
        {
            _itemViewer.Init(itemImage, rarityImage, level, rarityLevel);
        }
    }

    /// <summary>
    /// 슬롯을 눌렀을 시 실행되는 함수
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        OnItemSlotClicked?.Invoke(_itemData);
    }
}
