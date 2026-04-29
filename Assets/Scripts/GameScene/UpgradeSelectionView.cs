using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeSelectionView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _descTexc;

    private Button _btn;
    private IUpgradable _upgradable;

    public event UnityAction OnSelect;

    public void Init(IUpgradable upgradable)
    {
        _btn = GetComponent<Button>();

        _btn.onClick.AddListener(HandleOnClicked);

        _upgradable = upgradable;

        OnSelect += _upgradable.Upgrade;

        _icon.sprite = _upgradable.IconSprite;
        _levelText.text = $"Lv. {_upgradable.Level} > {_upgradable.Level + 1}";
        _descTexc.text = _upgradable.Desc;
    }

    private void HandleOnClicked()
    {
        OnSelect?.Invoke();
    }
}
