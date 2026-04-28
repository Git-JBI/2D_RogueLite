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

    private void Awake()
    {
        _btn = GetComponent<Button>();
    }

    public void Init(IUpgradable upgradable)
    {
        _btn.onClick.AddListener(HandleOnClicked);

        _upgradable = upgradable;

        _icon.sprite = _upgradable.IconSprite;
        _levelText.text = _upgradable.Level.ToString();
        _descTexc.text = _upgradable.Desc;
    }

    private void HandleOnClicked()
    {
        OnSelect?.Invoke();
    }
}
