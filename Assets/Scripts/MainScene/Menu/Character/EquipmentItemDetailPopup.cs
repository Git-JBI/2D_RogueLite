using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 장비 아이템 세부 내용 팝업
/// </summary>
public class EquipmentItemDetailPopup : MonoBehaviour, IPointerClickHandler
{
    private EquipmentItemData _EquipmentItemData;

    public void Init(EquipmentItemData data)
    {
        _EquipmentItemData = data;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
    }
}
