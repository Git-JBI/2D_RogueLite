using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemDetailPopupViewer : MonoBehaviour
{
    //TODO 추후 텍스트로 나타내는것이 아님
    /*
    아이템 이름
    아이템 타입
    아이템 티어 
    아이템 레어리티
    아이템 레어리티 레벨
    아이템 레벨
    아이템 설명
     */
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _equipButtonText;

    public void Init(string itemName, EquipmentType type, ItemRarity rarity, int rarityLevel, int itemLevel, string desc)
    {
        _text.text = $"Name : {itemName}\n" +
            $"Type : {type}\n" +
            $"Rarity : {rarity}\n" +
            $"RarityLevel : {rarityLevel}\n" +
            $"Level : {itemLevel}\n" +
            $"Desc : {desc}";
    }

    public void SetEquipText()
    {
        _equipButtonText.text = "장착";
    }

    public void SetUnEquipText()
    {
        _equipButtonText.text = "장착 해제";
    }
}
