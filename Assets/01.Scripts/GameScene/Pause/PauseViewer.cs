using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseViewer : ButtonPressScale
{
    [Header("----- 패널 오브젝트 -----")]
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _homePanel;

    [Header("----- 버튼 -----")]
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _pauseResumeButton;
    [SerializeField] private Button _homeResumeButton;

    [Header("----- Toggle -----")]
    [SerializeField] private Toggle _bgmToggle;
    [SerializeField] private Toggle _sfxToggle;
    [SerializeField] private Toggle _damageFloaterToggle;

    [Header("----- ToggleViewer -----")]
    [SerializeField] private ToggleViewer _bgmToggleViewer;
    [SerializeField] private ToggleViewer _sfxToggleViewer;
    [SerializeField] private ToggleViewer _damageFloaterToggleViewer;

    public event UnityAction OnPauseButtonClicked;
    public event UnityAction OnPauseResumeButtonClicked;
    public event UnityAction OnBgmToggleClicked;
    public event UnityAction OnSfxToggleClicked;
    public event UnityAction OnDamageFloaterToggleClicked;

    private void Awake()
    {
        _pauseButton.onClick.AddListener(PauseButtonClick);
        _homeButton.onClick.AddListener(HomeButtonClick);
        _pauseResumeButton.onClick.AddListener(PauseResumeButtonClick);

        _bgmToggle.onValueChanged.AddListener(BgmToggleClick);
    }
    
    private void PauseButtonClick()
    {
        OnPauseButtonClicked?.Invoke();
        _pausePanel.SetActive(true);
    }
    private void HomeButtonClick()
    {
        _homePanel.SetActive(true);
    }
    private void PauseResumeButtonClick()
    {
        _pausePanel.SetActive(false);
    }
    private void BgmToggleClick(bool value)
    {

    }
}
