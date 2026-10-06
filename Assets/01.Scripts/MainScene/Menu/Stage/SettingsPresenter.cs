using UnityEngine;

public class SettingsPresenter : MonoBehaviour
{
    [SerializeField] private SettingsViewer _viewer;
    private AppConfigModel _configModel;

    public void Awake()
    {
        _configModel = GameManager.Instance.GameModel.AppConfigModel;

        _viewer.Init(_configModel.GetBgmEnable(), _configModel.GetSfxEnable(), _configModel.GetDamageFloaterEnable());

        _viewer.OnBgmToggleClicked += SetBgmConfigData;
        _viewer.OnSfxToggleClicked += SetSfxConfigData;
        _viewer.OnDamageFloaterToggleClicked += SetDamageFloaterConfigData;
    }

    private void SetBgmConfigData(bool value)
    {
        _configModel.SetBgmEnable(value);
    }
    private void SetSfxConfigData(bool value)
    {
        _configModel.SetSfxEnable(value);
    }
    private void SetDamageFloaterConfigData(bool value)
    {
        _configModel.SetDamageFloaterEnable(value);
    }
}
