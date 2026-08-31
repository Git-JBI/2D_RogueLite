using System.Collections.Generic;

public class EquipmentModel
{
    private GameData _gameData;
    private GameModel _gameModel;

    public EquipmentModel(GameData gameData, GameModel gameModel)
    {
        _gameData = gameData;
        _gameModel = gameModel;
    }

    public IReadOnlyList<EquipmentItemData> EquipmentItems => _gameData.EquipmentItems;
    public IReadOnlyList<EquipmentItemData> EquippedItems => _gameData.EquippedItems;

    /// <summary>
    /// 장비 아이템 리스트에 추가하는 함수
    /// </summary>
    /// <param name="data"></param>
    public void AddEquipmentList(EquipmentItemData data)
    {
        _gameData.EquipmentItems.Add(data);
    }
    /// <summary>
    /// 장비 아이템 리스트에서 제거하는 함수
    /// </summary>
    /// <param name="data"></param>
    public void RemoveEquipmentList(EquipmentItemData data)
    {
        _gameData.EquipmentItems.Remove(data);
    }
    /// <summary>
    /// 장비 아이템 리스트에서 instancecId를 이용해 아이템을 찾는 함수
    /// </summary>
    /// <param name="instanceId">인스턴스 ID</param>
    /// <returns></returns>
    public EquipmentItemData GetEquipmentItem(string instanceId)
    {
        EquipmentItemData item = _gameData.EquipmentItems.Find(x => x.InstanceID == instanceId);
        // 아이템을 찾지 못한 경우
        if (item == null)
            return null;
        // 아이템을 찾은 경우
        else
            return item;
    }
    /// <summary>
    /// 장착 중인 장비 아이템 리스트에 추가하는 함수
    /// </summary>
    /// <param name="data"></param>
    public void AddEquippedItemList(EquipmentItemData data)
    {
        _gameData.EquippedItems.Add(data);
    }
    /// <summary>
    /// 장착 중인 장비 아이템 리스트에서 제거하는 함수
    /// </summary>
    /// <param name="data"></param>
    public void RemoveEquippedItemList(EquipmentItemData data)
    {
        _gameData.EquippedItems.Remove(data);
    }
    /// <summary>
    /// 장착 중인 장비 아이템 리스트에서 EquipmentType을 이용해 아이템을 찾는 함수
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public EquipmentItemData GetEquippedItem(EquipmentType type)
    {
        EquipmentItemData item = _gameData.EquippedItems.Find(x => x.EquipmentType == type);
        // 아이템을 찾지 못한 경우
        if (item == null)
            return null;
        // 아이템을 찾은 경우
        else
            return item;
    }

    public void SaveData()
    {
        _gameModel.SaveGameData();
    }
}
