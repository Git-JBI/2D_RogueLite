using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseViewer : ButtonPressScale
{
    [Header("----- 패널 오브젝트 -----")]
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _homePanel;

    [Header("----- 버튼 -----")]
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _finishButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _homeResumeButton;

    [Header("----- Toggle -----")]
    [SerializeField] private Toggle _bgmToggle;
    [SerializeField] private Toggle _sfxToggle;
    [SerializeField] private Toggle _damageFloaterToggle;

    [Header("----- ToggleViewer -----")]
    [SerializeField] private ToggleViewer _bgmToggleViewer;
    [SerializeField] private ToggleViewer _sfxToggleViewer;
    [SerializeField] private ToggleViewer _damageFloaterToggleViewer;

    public event UnityAction OnResumeButtonClicked;
    public event UnityAction OnHomeResumeButtonClicked;
    public event UnityAction OnFinishButtonClicked;

    public event UnityAction<bool> OnBgmToggleClicked;
    public event UnityAction<bool> OnSfxToggleClicked;
    public event UnityAction<bool> OnDamageFloaterToggleClicked;

    private void Awake()
    {
        _homeButton.onClick.AddListener(HomeButtonClick);
        _resumeButton.onClick.AddListener(PauseResumeButtonClick);
        _homeResumeButton.onClick.AddListener(HomeResumeButtonClick);
        _finishButton.onClick.AddListener(FinishButtonClick);

        _bgmToggle.onValueChanged.AddListener(BgmToggleClick);
        _sfxToggle.onValueChanged.AddListener(SfxToggleClick);
        _damageFloaterToggle.onValueChanged.AddListener(DamageFloaterToggleClick);
    }
    
    public void Init(AppConfigModel model)
    {
        _bgmToggle.isOn = model.GetBgmEnable();
        _sfxToggle.isOn = model.GetSfxEnable();
        _damageFloaterToggle.isOn = model.GetDamageFloaterEnable();

        _bgmToggleViewer.Init(model.GetBgmEnable());
        _sfxToggleViewer.Init(model.GetSfxEnable());
        _damageFloaterToggleViewer.Init(model.GetDamageFloaterEnable());
    }

    private void HomeButtonClick()
    {
        _homePanel.SetActive(true);
    }
    private void PauseResumeButtonClick()
    {
        OnResumeButtonClicked?.Invoke();
        _pausePanel.SetActive(false);
    }
    private void HomeResumeButtonClick()
    {
        OnHomeResumeButtonClicked?.Invoke();
        _homePanel.SetActive(false);
    }
    private void FinishButtonClick()
    {
        OnFinishButtonClicked?.Invoke();
    }
    private void BgmToggleClick(bool value)
    {
        OnBgmToggleClicked?.Invoke(value);
        _bgmToggleViewer.SetImage(value);
    }
    private void SfxToggleClick(bool value)
    {
        OnSfxToggleClicked?.Invoke(value);
        _sfxToggleViewer.SetImage(value);
    }
    private void DamageFloaterToggleClick(bool value)
    {
        OnDamageFloaterToggleClicked?.Invoke(value);
        _damageFloaterToggleViewer.SetImage(value);
    }
}
