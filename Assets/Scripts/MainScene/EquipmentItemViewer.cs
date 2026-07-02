using TMPro;
using UnityEngine;
using UnityEngine.UI;

//TODO EquipmentItemViewer는 ItemViewer를 상속 받은 상태로 써야함
public class EquipmentItemViewer : ItemViewer
{
    [SerializeField] private Image _gradeImage;
    [SerializeField] private TextMeshProUGUI _descText;
    [SerializeField] private TextMeshProUGUI _rarityLevelText;
    [SerializeField] private TextMeshProUGUI _itemLevelText;

    public override void Init(EquipmentItemData data, string desc)
    {
        base.Init(data, desc);
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
