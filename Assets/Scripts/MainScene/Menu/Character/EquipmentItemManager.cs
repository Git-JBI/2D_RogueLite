using UnityEngine;

public class EquipmentItemManager : MonoBehaviour
{
    /// <summary>
    /// 장비 아이템 세부내용을 보여줄 팝업
    /// </summary>
    [SerializeField] private EquipmentItemDetailPopup _popup;

    /// <summary>
    /// 0. 무기 1. 목걸이 2. 장갑 3. 갑옷 4. 벨트 5. 신발
    /// </summary>
    private EquipmentItemSlot[] _slots;

    private void Awake()
    {
        _slots = GetComponentsInChildren<EquipmentItemSlot>();

        _popup.OnPopupClosed += Refresh;
    }

    public void Refresh()
    {
        Debug.Log(GameManager.Instance.GameModel.EquipmentModel.EquippedItems.Count);
        for (int i = 0; i < _slots.Length; i++)
        {
            EquipmentItemData itemdata = GameManager.Instance.GameModel.EquipmentModel.GetEquippedItem((EquipmentType)i);

            if (itemdata != null)
            {
                _slots[i].gameObject.SetActive(true);
                _slots[i].Init(itemdata);
                _slots[i].OnItemSlotClicked += OpenPopup;
            }
            else
                _slots[i].gameObject.SetActive(false);
        }
    }

    private void OpenPopup(EquipmentItemData data)
    {
        _popup.gameObject.SetActive(true);
        _popup.Init(data);
        _popup.SetEquippedItem();
    }
}