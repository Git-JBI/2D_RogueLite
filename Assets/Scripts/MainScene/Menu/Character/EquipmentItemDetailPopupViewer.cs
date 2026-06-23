using TMPro;
using Unity.VisualScripting;
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
    /// <summary>
    /// 세부 사항 내용 텍스트
    /// </summary>
    [SerializeField] private TextMeshProUGUI _descText;
    /// <summary>
    /// 아이템 레벨 텍스트
    /// </summary>
    [SerializeField] private TextMeshProUGUI _itemLevelText;
    /// <summary>
    /// 장비 장착/탈착 버튼 텍스트
    /// </summary>
    [SerializeField] private TextMeshProUGUI _equipButtonText;
    /// <summary>
    /// 업그레이드 시 필요한 골드 텍스트
    /// </summary>
    [SerializeField] private TextMeshProUGUI _goldCostText;
    /// <summary>
    /// 업그레이드 시 필요한 스크롤 텍스트
    /// </summary>
    [SerializeField] private TextMeshProUGUI _scrollCostText;
    /// <summary>
    /// 최대 레벨일 시 나타날 텍스트
    /// </summary>
    [SerializeField] private TextMeshProUGUI _maxUpgradeText;

    public void Init(EquipmentItemData itemData, SO_EquipmentItemData itemConfigData, int requireGold, int requireScroll)
    {
        _descText.text = $"Name : {itemConfigData.ItemName}\n" +
            $"Type : {itemData.EquipmentType}\n" +
            $"Rarity : {itemData.Rarity}\n" +
            $"RarityLevel : {itemData.RarityLevel}\n" +
            $"Desc : {itemConfigData.Desc}";

        SetGoldCostText(GameManager.Instance.GameModel.Gold, requireGold);
        SetScrollCostText(GameManager.Instance.GameModel.UpgradeScroll[(int)itemData.EquipmentType], requireScroll);
    }

    public void SetEquipText()
    {
        _equipButtonText.text = "장착";
    }

    public void SetUnEquipText()
    {
        _equipButtonText.text = "장착 해제";
    }

    public void SetGoldCostText(int haveGold, int require)
    {
        string text;
        if(require > haveGold)
            text = "<color=red>" + haveGold.ToAbbreviatedString() + "</color>" + "/" + require.ToAbbreviatedString();
        else
            text = haveGold.ToAbbreviatedString() + "/" + require.ToAbbreviatedString();
        _goldCostText.text = text;
    }

    public void SetScrollCostText(int haveScroll, int require)
    {
        string text;
        if (require > haveScroll)
            text = $"<color=red>{haveScroll}</color> / {require}";
        else
            text = $"{haveScroll} / {require}";

        _scrollCostText.text = text;
    }

    public void CheckMaxLevel(bool value)
    {
        if(value)
        {
            _goldCostText.gameObject.SetActive(!value);
            _scrollCostText.gameObject.SetActive(!value);
            _maxUpgradeText.gameObject.SetActive(value);
        }
        else
        {
            _goldCostText.gameObject.SetActive(!value);
            _scrollCostText.gameObject.SetActive(!value);
            _maxUpgradeText.gameObject.SetActive(value);
        }
    }

    public void SetItemLevelText(int level, int maxLevel)
    {
        _itemLevelText.text = $"레벨: {level} / {maxLevel}";
    }
}