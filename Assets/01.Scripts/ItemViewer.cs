using UnityEngine;
using UnityEngine.UI;

public class ItemViewer : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _rarityImage;

    public void Init(Sprite itemImage, Sprite rarityImage)
    {
        SetItemSprite(itemImage);
        SetRaritySprite(rarityImage);
    }

    private void SetRaritySprite(Sprite sprite)
    {
        _rarityImage.sprite = sprite;
    }

    private void SetItemSprite(Sprite sprite)
    {
        _itemImage.sprite = sprite;
    }
}
