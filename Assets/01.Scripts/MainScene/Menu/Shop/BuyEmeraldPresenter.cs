using UnityEngine;

public class BuyEmeraldPresenter : MonoBehaviour
{
    // 100에메 - 1000원
    // 500에메 - 5000원
    // 1100에메 - 9900원
    // 3000에메 - 24900원
    // 7000에메 - 49000원
    // 15000에메 - 99000원
    private BuyViewerBase _viewer;
    private GameModel _gameModel;
    private SO_CurrencyPurchaseConfigData _configData;

    private void Awake()
    {
        _viewer = GetComponent<BuyViewerBase>();
        _gameModel = GameManager.Instance.GameModel;
        _configData = GameManager.Instance.ResourceManager.GetScriptable("GameDatas/MainScene/EmeraldPurchaseConfigData/EmeraldPurchaseConfigData") as SO_CurrencyPurchaseConfigData;
    }

    private void Start()
    {
        for(int i = 0; i < _configData.Rewards.Length; i++)
        {
            _viewer.SetRewardText(i, _configData.Rewards[i]);
            _viewer.SetRequireText(i, _configData.Requires[i]);
        }
    }

    private void OnEnable()
    {
        _viewer.OnButtonsClicked += PurchaseEmerald;
    }
    private void OnDisable()
    {
        _viewer.OnButtonsClicked -= PurchaseEmerald;
    }

    private void PurchaseEmerald(int index)
    {
        if (index < 0 || index >= _configData.Requires.Length)
            return;

        //TODO Requires에 대한 내용도 추가로 필요(필요 요금)

        _gameModel.AddEmerald(_configData.Rewards[index]);
        _gameModel.SaveGameData();
    }
}
