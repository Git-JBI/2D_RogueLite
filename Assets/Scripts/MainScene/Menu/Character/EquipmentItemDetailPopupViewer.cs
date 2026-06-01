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
    [SerializeField] private TextMeshProUGUI _descText;
    [SerializeField] private TextMeshProUGUI _itemLevelText;
    [SerializeField] private TextMeshProUGUI _equipButtonText;
    [SerializeField] private TextMeshProUGUI _goldCostText;
    [SerializeField] private TextMeshProUGUI _scrollCostText;

    public void Init(string itemName, EquipmentType type, ItemRarity rarity, int rarityLevel, string desc, int requireGold, int requireScroll)
    {
        _descText.text = $"Name : {itemName}\n" +
            $"Type : {type}\n" +
            $"Rarity : {rarity}\n" +
            $"RarityLevel : {rarityLevel}\n" +
            $"Desc : {desc}";

        SetGoldCostText(requireGold, GameManager.Instance.GameModel.Gold);
        SetScrollCostText(requireScroll, GameManager.Instance.GameModel.UpgradeScroll[(int)type]);
    }

    public void SetEquipText()
    {
        _equipButtonText.text = "장착";
    }

    public void SetUnEquipText()
    {
        _equipButtonText.text = "장착 해제";
    }

    public void SetGoldCostText(int require, int haveGold)
    {
        string text = require.ToAbbreviatedString() + "/" + haveGold.ToAbbreviatedString();
        _goldCostText.text = text;
    }

    public void SetScrollCostText(int require, int haveScroll)
    {
        _scrollCostText.text = $"{require} / {haveScroll}";
    }

    public void SetItemLevelText(int level, int maxLevel)
    {
        _itemLevelText.text = $"레벨: {level} / {maxLevel}";
    }
}