using TMPro;

public enum UpgradeButtonType
{
    None,
    Damage,
    Hp,
    Armor,
}

public class Button_Upgrade : ButtonBase
{
    private UpgradeManager _manager;

    private UpgradeButtonType _upgradeType;
    private TextMeshProUGUI[] _texts;           // 0.Title, 1.Level, 2.Money

    protected override void Awake()
    {
        base.Awake();

        _texts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void Init(UpgradeManager manager, UpgradeButtonType value)
    {
        _manager = manager;
        _upgradeType = value;

        base.Init();
    }

    public void SetText(int level, int cost)
    {
        _texts[1].text = $"Lv.{level + 1}";
        _texts[2].text = $"{cost}";
    }

    public void SetMaxLevel()
    {
        _texts[1].text = "MAX";
        _texts[2].text = "-";
    }

    protected override void ClickButton()
    {
        _manager.Upgrade(_upgradeType);
    }
}
