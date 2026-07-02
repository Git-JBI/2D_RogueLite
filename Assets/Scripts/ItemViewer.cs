using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewer : MonoBehaviour
{
    [SerializeField] private Image _gradeImage;
    [SerializeField] private TextMeshProUGUI _descText;
    private SO_RarityImageColorData _colorData;

    public virtual void Init(EquipmentItemData data, string desc)
    {
        if (!_colorData)
            _colorData = GameManager.Instance.ResourceManager.GetScriptable("GameDatas/RarityImageColorData/RarityImageColorData") as SO_RarityImageColorData;

        SetGradeColor(data.Rarity);
        SetDescText(desc);
    }

    private void SetGradeColor(ItemRarity rarity)
    {
        switch(rarity)
        {
            case ItemRarity.Common:
                _gradeImage.color = _colorData.Colors[(int)ItemRarity.Common];
                break;
            case ItemRarity.UnCommon:
                _gradeImage.color = _colorData.Colors[(int)ItemRarity.UnCommon];
                break;
            case ItemRarity.Rare:
                _gradeImage.color = _colorData.Colors[(int)ItemRarity.Rare];
                break;
            case ItemRarity.Unique:
                _gradeImage.color = _colorData.Colors[(int)ItemRarity.Unique];
                break;
            case ItemRarity.Epic:
                _gradeImage.color = _colorData.Colors[(int)ItemRarity.Epic];
                break;
            case ItemRarity.Legendary:
                _gradeImage.color = _colorData.Colors[(int)ItemRarity.Legendary];
                break;
        }
    }

    private void SetDescText(string desc)
    {
        _descText.text = desc;
    }
}
