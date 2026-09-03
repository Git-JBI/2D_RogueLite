using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeButtonViewer : ButtonBase
{
    [SerializeField] private TMP_Text _upgradeNameText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _priceText;

    public event UnityAction<int> OnButtonClicked;

    private UpgradeButtonType _upgradeType;

    public void Init(UpgradePresenter manager, UpgradeButtonType value)
    {
        _upgradeType = value;
    }

    public void SetText(int level, int cost)
    {
        _levelText.text = $"Lv.{level + 1}";
        _priceText.text = $"{cost}";
    }

    public void SetMaxLevel()
    {
        _levelText.text = "MAX";
        _priceText.text = "-";
    }
}
