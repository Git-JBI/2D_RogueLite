using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemViewer : MonoBehaviour
{
    private Image _image;
    private TextMeshProUGUI _text;

    public void Init(ItemRarity rarity, string desc)
    {
        _image = GetComponentInChildren<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();

        SetImage(rarity);
        SetText(desc);
    }

    public void SetImage(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Common:
                _image.color = Color.gray;
                break;
            case ItemRarity.UnCommon:
                _image.color = Color.green;
                break;
            case ItemRarity.Rare:
                _image.color = Color.blue;
                break;
            case ItemRarity.Unique:
                _image.color = Color.magenta;
                break;
            case ItemRarity.Epic:
                _image.color = Color.yellow;
                break;
            case ItemRarity.Legendary:
                _image.color = Color.red;
                break;
        }
    }

    public void SetText(string desc)
    {
        _text.text = desc;
    }
}
