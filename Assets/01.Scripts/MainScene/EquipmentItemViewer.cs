using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemViewer : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _squareRarityImage;
    [SerializeField] private Image _circleRarityImage;
    [SerializeField] private Image _itemTypeImage;
    [SerializeField] private Image _diamondRarityImage;
    [SerializeField] private TextMeshProUGUI _rarityLevelText;
    [SerializeField] private TextMeshProUGUI _itemLevelText;
    [SerializeField] private TMP_Text _itemLevelShadowText;

    public void Init(Sprite itemSprite, Sprite itemTypeSprite, Sprite squareRaritySprite, Sprite circleRaritySprite, Sprite diamondRaritySprite, int level, int rarityLevel)
    {
        SetItemSprite(itemSprite);
        SetItemTypeSprite(itemTypeSprite);
        SetRaritySprite(squareRaritySprite, circleRaritySprite);
        SetItemLevelText(level.ToString());
        SetRarityLevel(diamondRaritySprite, rarityLevel);
    }
    private void SetRaritySprite(Sprite squareSprite, Sprite circleSprite)
    {
        _squareRarityImage.sprite = squareSprite;
        _circleRarityImage.sprite = circleSprite;
    }
    private void SetRarityLevel(Sprite diamondSprite, int rarityLevel)
    {
        bool isVisible = rarityLevel >= 1;

        _diamondRarityImage.sprite = diamondSprite;
        _rarityLevelText.text = isVisible ? rarityLevel.ToString() : "";

        _diamondRarityImage.gameObject.SetActive(isVisible);
        _rarityLevelText.gameObject.SetActive(isVisible);
    }
    private void SetItemTypeSprite(Sprite sprite)
    {
        _itemTypeImage.sprite = sprite;
    }
    private void SetItemSprite(Sprite sprite)
    {
        _itemImage.sprite = sprite;
    }
    private void SetItemLevelText(string itemLevel)
    {
        _itemLevelText.text = $"Lv.{itemLevel}";
        _itemLevelShadowText.text = $"Lv.{itemLevel}";
    }
}
