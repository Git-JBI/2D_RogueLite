using System.Collections.Generic;
using UnityEngine;

public class EquipmentModel
{
    private GameData _gameData;
    private GameModel _gameModel;

    public EquipmentModel(GameData gameData, GameModel gameModel)
    {
        _gameData = gameData;
        _gameModel = gameModel;
    }

    public List<EquipmentItemData> EquipmentItems => _gameData.EquipmentItems;
    public List<EquipmentItemData> EquippedItems => _gameData.EquippedItems;
}
