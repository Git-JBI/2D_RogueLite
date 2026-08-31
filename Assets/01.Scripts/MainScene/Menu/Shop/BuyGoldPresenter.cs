using UnityEngine;

public class BuyGoldPresenter : MonoBehaviour
{
    private BuyViewerBase _viewer;
    private GameModel _gameModel;
    private SO_CurrencyPurchaseConfigData _configData;

    private void Awake()
    {
        _viewer = GetComponent<BuyViewerBase>();
        _gameModel = GameManager.Instance.GameModel;
        _configData = GameManager.Instance.ResourceManager.GetScriptable("GameDatas/MainScene/GoldPurchaseConfigData/GoldPurchaseConfigData") as SO_CurrencyPurchaseConfigData;
    }

    private void Start()
    {
        _viewer.Init(_configData);
    }

    private void OnEnable()
    {
        _viewer.OnButtonsClicked += PurchaseGold;
    }
    private void OnDisable()
    {
        _viewer.OnButtonsClicked -= PurchaseGold;
    }

    private void PurchaseGold(int index)
    {
        if (index < 0 || index >= _configData.Requires.Length)
            return;

        // 필요 에메랄드가 적을 경우
        if (_gameModel.Emerald < _configData.Requires[index])
            return;

        // 에메랄드 감소
        _gameModel.AddEmerald(-_configData.Requires[index]);

        // 스테이지별 1시간당 얻는 골드양
        int gold = 6000 + (_gameModel.MaxStage - 1) * 600;

        switch(index)
        {
            case 0:
                gold *= 2;
                break;
            case 1:
                gold *= 8;
                break;
            case 2:
                gold *= 24;
                break;
        }

        // 골드 추가
        _gameModel.AddGold(gold);

        // 저장
        _gameModel.SaveGameData();
    }
}
