using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 장비 아이템 세부 내용 팝업
/// </summary>
public class EquipmentItemDetailPopup : MonoBehaviour
{
    [SerializeField] private Button _outside;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _equipButton;

    private EquipmentItemData _equipmentItemData;
    private EquipmentItemDetailPopupViewer _popupViewer;

    public event UnityAction OnPopupClosed;

    private void Awake()
    {
        _outside.onClick.AddListener(PopupClosed);
        _closeButton.onClick.AddListener(PopupClosed);
    }

    public void Init(EquipmentItemData data)
    {
        if(!_popupViewer)
            _popupViewer = GetComponentInChildren<EquipmentItemDetailPopupViewer>();

        _equipmentItemData = data;

        Debug.Log(data.ItemID);

        string itemName = GameManager.Instance.EquipmentItemDatabase.GetEquipmentItemName(data.ItemID);
        string itemDesc = GameManager.Instance.EquipmentItemDatabase.GetEquipmentItemDesc(data.ItemID);
        _popupViewer.Init(itemName,_equipmentItemData.EquipmentType, _equipmentItemData.Rarity, _equipmentItemData.RarityLevel, _equipmentItemData.ItemLevel, itemDesc);
    }

    private void PopupClosed()
    {
        OnPopupClosed?.Invoke();

        gameObject.SetActive(false);
    }
}
