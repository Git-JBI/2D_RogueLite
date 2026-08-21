using UnityEngine;

public class UpperSideUiPresenter : MonoBehaviour
{
    private GameModel _gameModel;
    private EnergyManager _energyManager;
    private UpperSideUiViewer _viewer;

    public void Awake()
    {
        _gameModel = GameManager.Instance.GameModel;
        _energyManager = GameManager.Instance.EnergyManager;

        _viewer = GetComponent<UpperSideUiViewer>();

        Init();

        _energyManager.OnEnergyChanged += _viewer.SetEnergyText;
        _energyManager.OnTimeChanged += _viewer.SetTimeText;
    }

    public void OnDestroy()
    {
        _energyManager.OnEnergyChanged -= _viewer.SetEnergyText;
        _energyManager.OnTimeChanged -= _viewer.SetTimeText;
    }

    private void Init()
    {
        _viewer.SetTimeText(_energyManager.EnergyRechargeRemainingTime);
        _viewer.SetEnergyText(_energyManager.Energy, _energyManager.MaxEnergy);
        _viewer.SetEmeraldText(_gameModel.Emerald);
        _viewer.SetGoldText(_gameModel.Gold);
    }
}
