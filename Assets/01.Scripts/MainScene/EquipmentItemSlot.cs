using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EquipmentItemSlot : MonoBehaviour, IPointerClickHandler
{
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

    public void Init(EquipmentItemData data)
    {
        if(!_itemViewer)
            _itemViewer = GetComponent<EquipmentItemViewer>();

        OnItemSlotClicked = null;

        _itemData = data;

        EquipmentItemDatabase database = GameManager.Instance.EquipmentItemDatabase;

        _itemViewer.Init(database.GetEquipmentItemSprite(data.ItemID, data.Rarity),
            database.GetItemTypeSprite(data.EquipmentType),
            database.GetSquareRaritySprite(data.Rarity),
            database.GetCircleRaritySprite(data.Rarity),
            database.GetDiamondRaritySprite(data.Rarity),
            data.ItemLevel,
            data.RarityLevel);
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
