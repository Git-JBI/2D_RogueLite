using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewer : MonoBehaviour
{
    private Image _gradeImage;
    private TextMeshProUGUI _descText;

    private void Awake()
    {
        _gradeImage = GetComponentInChildren<Image>();
        _descText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Init(ItemRarity rarity, string desc)
    {
        SetGradeColor(rarity);
        SetDescText(desc);
    }

    private void SetGradeColor(ItemRarity rarity)
    {
        switch(rarity)
        {
            case ItemRarity.Common:
                _gradeImage.color = Color.gray;
                break;
            case ItemRarity.UnCommon:
                _gradeImage.color = Color.green;
                break;
            case ItemRarity.Rare:
                _gradeImage.color = Color.blue;
                break;
            case ItemRarity.Unique:
                _gradeImage.color = Color.magenta;
                break;
            case ItemRarity.Epic:
                _gradeImage.color = Color.yellow;
                break;
            case ItemRarity.Legendary:
                _gradeImage.color = Color.red;
                break;
        }
    }

    private void SetDescText(string desc)
    {
        _descText.text = desc;
    }
}
