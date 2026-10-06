using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsViewer : MonoBehaviour
{
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private Button _backgroundButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Toggle _bgmToggle;
    [SerializeField] private Toggle _sfxToggle;
    [SerializeField] private Toggle _damageFloaterToggle;
    [SerializeField] private ToggleViewer _bgmToggleManager;
    [SerializeField] private ToggleViewer _sfxToggleManager;
    [SerializeField] private ToggleViewer _damageFloaterToggleManager;

    public event UnityAction<bool> OnBgmToggleClicked;
    public event UnityAction<bool> OnSfxToggleClicked;
    public event UnityAction<bool> OnDamageFloaterToggleClicked;

    public void Awake()
    {
        _backgroundButton.onClick.AddListener(ClosePanel);
        _closeButton.onClick.AddListener(ClosePanel);

        _bgmToggle.onValueChanged.AddListener(ClickBgmToggle);
        _sfxToggle.onValueChanged.AddListener(ClickSfxToggle);
        _damageFloaterToggle.onValueChanged.AddListener(ClickDamagaFloaterToggle);
    }

    public void Init(bool bgm, bool sfx, bool damage)
    {
        _bgmToggle.isOn = bgm;
        _sfxToggle.isOn = sfx;
        _damageFloaterToggle.isOn = damage;

        _bgmToggleManager.Init(_bgmToggle.isOn);
        _sfxToggleManager.Init(_sfxToggle.isOn);
        _damageFloaterToggleManager.Init(_damageFloaterToggle.isOn);
    }
    
    private void ClosePanel()
    {
        _settingPanel.gameObject.SetActive(false);
    }
    private void ClickBgmToggle(bool value)
    {
        OnBgmToggleClicked?.Invoke(value);
    }
    private void ClickSfxToggle(bool value)
    {
        OnSfxToggleClicked?.Invoke(value);
    }
    private void ClickDamagaFloaterToggle(bool value)
    {
        OnDamageFloaterToggleClicked?.Invoke(value);
    }
}
