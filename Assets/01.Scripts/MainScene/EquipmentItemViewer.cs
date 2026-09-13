using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemViewer : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _rarityImage;
    [SerializeField] private TextMeshProUGUI _rarityLevelText;
    [SerializeField] private TextMeshProUGUI _itemLevelText;
    [SerializeField] private TMP_Text _itemLevelShadowText;

    public void Init(Sprite itemSprite, Sprite raritySprite, int level, int rarityLevel)
    {
        SetItemSprite(itemSprite);
        SetRaritySprite(raritySprite);
        SetItemLevelText(level.ToString());
        SetRarityLevelText(rarityLevel);
    }
    public void SetRaritySprite(Sprite sprite)
    {
        _rarityImage.sprite = sprite;
    }
    public void SetItemSprite(Sprite sprite)
    {
        _itemImage.sprite = sprite;
    }
    private void SetItemLevelText(string itemLevel)
    {
        _itemLevelText.text = $"Lv.{itemLevel}";
        _itemLevelShadowText.text = $"Lv.{itemLevel}";
    }
    private void SetRarityLevelText(int rarityLevel)
    {
        if (rarityLevel == 0)
            _rarityLevelText.text = "";
        else
            _rarityLevelText.text = rarityLevel.ToString();
    }
}
