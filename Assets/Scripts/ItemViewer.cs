using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewer : MonoBehaviour
{
    private Image _gradeImage;
    private TextMeshProUGUI _descText;

    public void Init(ItemRarity rarity, string desc)
    {
        if(!_gradeImage)
            _gradeImage = GetComponentInChildren<Image>();
        if(!_descText)
            _descText = GetComponentInChildren<TextMeshProUGUI>();


        SetGradeColor(rarity);
        SetDescText(desc);
    }

    private void SetGradeColor(ItemRarity rarity)
    {
        switch(rarity)
        {
            case ItemRarity.Common:
                _gradeImage.color = new Color(161, 165, 190);
                break;
            case ItemRarity.UnCommon:
                _gradeImage.color = new Color(150, 255, 0);
                break;
            case ItemRarity.Rare:
                _gradeImage.color = new Color(0, 180, 255);
                break;
            case ItemRarity.Unique:
                _gradeImage.color = new Color(203, 36, 254);
                break;
            case ItemRarity.Epic:
                _gradeImage.color = new Color(255, 251, 6);
                break;
            case ItemRarity.Legendary:
                _gradeImage.color = new Color(255, 89, 117);
                break;
        }
    }

    private void SetDescText(string desc)
    {
        _descText.text = desc;
    }
}
