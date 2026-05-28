using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private EquipmentItemDetailPopup _popup;

    /// <summary>
    /// 0. π´±‚ 1. ∏Ò∞…¿Ã 2. ¿Â∞© 3. ∞©ø  4. ∫ß∆Æ 5. Ω≈πﬂ
    /// </summary>
    private EquipmentItemSlot[] _slots;

    private void Awake()
    {
        _slots = GetComponentsInChildren<EquipmentItemSlot>();
    }

    public void Refresh()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].Init(GameManager.Instance.GameModel.EquippedItems.Find(x => x.EquipmentType == (EquipmentType)i));

        }
    }
}