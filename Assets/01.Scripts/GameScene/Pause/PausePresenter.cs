using UnityEngine;

public class PausePresenter : MonoBehaviour
{
    [SerializeField] private GameSceneManager _gameSceneManager;
    [SerializeField] private PauseViewer _viewer;
    private AppConfigModel _configModel;

    public void Awake()
    {
        _configModel = GameManager.Instance.GameModel.AppConfigModel;

        _viewer.Init(_configModel);
    }

    private void Start()
    {
        _viewer.OnResumeButtonClicked += _gameSceneManager.Resume;
        _viewer.OnFinishButtonClicked += _gameSceneManager.LoadMainScene;

        _viewer.OnBgmToggleClicked += _configModel.SetBgmEnable;
        _viewer.OnSfxToggleClicked += _configModel.SetSfxEnable;
        _viewer.OnDamageFloaterToggleClicked += _configModel.SetDamageFloaterEnable;
    }
}
