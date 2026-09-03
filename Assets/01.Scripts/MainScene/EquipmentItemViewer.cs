using TMPro;
using UnityEngine;

public class EquipmentItemViewer : ItemViewer
{
    [SerializeField] private TextMeshProUGUI _rarityLevelText;
    [SerializeField] private TextMeshProUGUI _itemLevelText;

    public void Init(EquipmentItemData data, string desc)
    {
        // base.Init(data, desc);
        SetItemLevelText(data.ItemLevel.ToString());
        SetRarityLevelText(data.RarityLevel);
    }

    private void SetItemLevelText(string itemLevel)
    {
        _itemLevelText.text = $"Lv.{itemLevel}";
    }

    private void SetRarityLevelText(int rarityLevel)
    {
        if (rarityLevel == 0)
            _rarityLevelText.text = "";
        else
            _rarityLevelText.text = rarityLevel.ToString();
    }
}
