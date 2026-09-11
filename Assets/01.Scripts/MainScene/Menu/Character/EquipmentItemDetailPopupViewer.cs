using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemDetailPopupViewer : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _rarityImage;
    /// <summary>
    /// 아이템 이름 텍스트
    /// </summary>
    [SerializeField] private TMP_Text _itemNameText;
    /// <summary>
    /// 아이템 설명 텍스트
    /// </summary>
    [SerializeField] private TMP_Text _descText;
    /// <summary>
    /// 아이템 레벨 텍스트
    /// </summary>
    [SerializeField] private TextMeshProUGUI _itemLevelText;

    [SerializeField] private Image _atkImage;
    [SerializeField] private Image _defImage;
    [SerializeField] private TMP_Text _upgradeValueText;

    [Header("----- 하단 -----")]
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
    /// 아이템 레벨이 최대 레벨일 시 나타날 텍스트
    /// </summary>
    [SerializeField] private TextMeshProUGUI _maxUpgradeText;

    public void SetItemImage(Sprite sprite)
    {
        _itemImage.sprite = sprite;
    }
    public void SetRarityImage(Sprite sprite)
    {
        _rarityImage.sprite = sprite;
    }
    public void SetItemNameText(string text)
    {
        _itemNameText.text = text;
    }
    public void SetUpgradeValueText(string text)
    {
        _upgradeValueText.text = $"+{text}";
    }
    public void SetEquipmentType(EquipmentType type)
    {
        if(type == EquipmentType.Weapon || type == EquipmentType.Necklace || type == EquipmentType.Gloves)
        {
            _atkImage.gameObject.SetActive(true);
            _defImage.gameObject.SetActive(false);
        }
        else
        {
            _atkImage.gameObject.SetActive(false);
            _defImage.gameObject.SetActive(true);
        }
    }
    public void SetDescText(string text)
    {
        _descText.text = text;
    }
    public void SetItemLevelText(int level, int maxLevel)
    {
        _itemLevelText.text = $"레벨: {level} / {maxLevel}";
    }

    public void RefreshUpgradeValueText(string text)
    {
        SetUpgradeValueText(text);

        Sequence sec = DOTween.Sequence();
        sec.Append(_upgradeValueText.transform.DOScale(1.2f, 0.1f));
        sec.Append(_upgradeValueText.transform.DOScale(1.0f, 0.1f));
    }
    public void RefreshLevelText(int level, int maxLevel)
    {
        SetItemLevelText(level, maxLevel);

        Sequence sec = DOTween.Sequence();
        sec.Append(_itemLevelText.transform.DOScale(1.2f, 0.1f));
        sec.Append(_itemLevelText.transform.DOScale(1.0f, 0.1f));
    }
    public void RefreshGoldCostText(int haveGold, int requireGold)
    {
        SetGoldCostText(haveGold, requireGold);

        Sequence sec = DOTween.Sequence();
        sec.Append(_goldCostText.transform.DOScale(1.2f, 0.1f));
        sec.Append(_goldCostText.transform.DOScale(1.0f, 0.1f));
    }
    public void RefreshScrollCostText(int haveScroll, int requireScroll)
    {
        SetScrollCostText(haveScroll, requireScroll);

        Sequence sec = DOTween.Sequence();
        sec.Append(_scrollCostText.transform.DOScale(1.2f, 0.1f));
        sec.Append(_scrollCostText.transform.DOScale(1.0f, 0.1f));
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
}