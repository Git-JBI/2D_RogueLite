using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public string HeroDataName;
    public int Gold;
    public int[] UpgradeLevel = { 0, 0, 0 };

    public List<EquipmentItemData> InventoryItems = new List<EquipmentItemData>();
    public List<EquipmentItemData> EquippedItems = new List<EquipmentItemData>();
    //TODO 일반 아이템 리스트 따로 필요(ex. 뽑기용 열쇠, 장비 강화용 주문서 등등)
}