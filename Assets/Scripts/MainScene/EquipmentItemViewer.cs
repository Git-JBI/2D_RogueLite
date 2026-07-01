using TMPro;
using UnityEngine;
using UnityEngine.UI;

//TODO EquipmentItemViewer는 ItemViewer를 상속 받은 상태로 써야함
public class EquipmentItemViewer : MonoBehaviour
{
    [SerializeField] private Image _gradeImage;
    [SerializeField] private TextMeshProUGUI _descText;
    [SerializeField] private TextMeshProUGUI _itemLevelText;

    public void Init(ItemRarity rarity, string desc)
    {
        SetImage(rarity);
        SetDesc(desc);
    }

    //TODO 컬러를 여기서 new로 하지말고 따로 ScriptableObject를 만든 후 설정 데이터 생성 후에 ResourceManager에서 받아오기
    public void SetImage(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Common:
                _gradeImage.color = new Color32(161, 165, 190, 255);
                break;
            case ItemRarity.UnCommon:
                _gradeImage.color = new Color32(150, 255, 0, 255);
                break;
            case ItemRarity.Rare:
                _gradeImage.color = new Color32(0, 180, 255, 255);
                break;
            case ItemRarity.Unique:
                _gradeImage.color = new Color32(203, 36, 254, 255);
                break;
            case ItemRarity.Epic:
                _gradeImage.color = new Color32(255, 251, 6, 255);
                break;
            case ItemRarity.Legendary:
                _gradeImage.color = new Color32(255, 89, 117, 255);
                break;
        }
    }

    public void SetDesc(string desc)
    {
        _descText.text = desc;
    }
}
