using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeSelection : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _descTexc;

    private Button _btn;

    public event UnityAction OnSelected;

    public void Init(IUpgradable upgradable)
    {
        _btn = GetComponent<Button>();

        _btn.onClick.AddListener(HandleOnClicked);

        OnSelected += upgradable.Upgrade;

        _icon.sprite = upgradable.IconSprite;

        if(upgradable.Level == 99)
        {
            _levelText.text = "";
        }
        else
        {
            _levelText.text = $"Lv. {upgradable.Level} > {upgradable.Level + 1}";
        }

        _descTexc.text = upgradable.Desc;
    }

    public void EmptyEvent()
    {
        _btn.onClick.RemoveAllListeners();
        OnSelected = null;
    }

    private void HandleOnClicked()
    {
        OnSelected?.Invoke();
    }
}
