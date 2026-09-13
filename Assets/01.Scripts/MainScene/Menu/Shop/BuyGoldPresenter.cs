using System;
using UnityEngine;

public class BuyGoldPresenter : MonoBehaviour
{
    private int _baseGold;
    private int _stageGold;

    private BuyViewerBase _viewer;
    private GameModel _gameModel;
    private SO_CurrencyPurchaseConfigData _configData;

    private void Awake()
    {
        _viewer = GetComponent<BuyViewerBase>();
        _gameModel = GameManager.Instance.GameModel;
        _configData = GameManager.Instance.ResourceManager.GetScriptable("GameDatas/MainScene/GoldPurchaseConfigData/GoldPurchaseConfigData") as SO_CurrencyPurchaseConfigData;

        _baseGold = 6000;

        _stageGold = (_gameModel.MaxStage - 1) * 600;
    }

    private void Start()
    {
        int gold;
        for (int i = 0; i < _configData.Rewards.Length; i++)
        {
            switch (i)
            {
                case 0:
                    gold = (_baseGold + _stageGold) * 2;
                    break;
                case 1:
                    gold = (_baseGold + _stageGold) * 8;
                    break;
                case 2:
                    gold = (_baseGold + _stageGold) * 24;
                    break;
                default:
                    return;
            }
            _viewer.SetRewardText(i, gold);
            _viewer.SetRequireText(i, _configData.Requires[i]);
        }
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

        int gold;

        switch(index)
        {
            case 0:
                gold = (_baseGold + _stageGold) * 2;
                break;
            case 1:
                gold = (_baseGold + _stageGold) * 8;
                break;
            case 2:
                gold = (_baseGold + _stageGold) * 24;
                break;
            default:
                return;
        }

        // 골드 추가
        _gameModel.AddGold(gold);

        // 저장
        _gameModel.SaveGameData();
    }
}
