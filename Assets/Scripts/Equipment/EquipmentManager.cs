using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private int _equipmentBonusAtk;
    private int _equipmentBonusHp;

    public int EquipmentBonusAtk => _equipmentBonusAtk;
    public int EquipmentBonusHp => _equipmentBonusHp;

    public void EquipItem(string instanceId)
    {
        GameModel gameModel = GameManager.Instance.GameModel;
        // 장비 인벤토리에서 장착할 아이템을 찾기
        EquipmentItemData item = gameModel.FindEquipmentList(instanceId);

        // 장착할 아이템의 타입
        EquipmentType type = item.EquipmentType;

        // 장착 중인 아이템
        EquipmentItemData equippedItem = gameModel.FindEquippedItemList(type);

        // 장착 중인 아이템이 있을 시
        if(equippedItem != null)
        {
            // 장착중인 장비 리스트에서 장착중인 장비 제거
            gameModel.RemoveEquippedItemList(equippedItem);
            // 보유중인 장비 리스트에 장착중이였던 장비 추가
            gameModel.AddEquipmentList(equippedItem);
            // 적용되어있던 장비 스텟 감소
        }

        // 보유중인 장비 리스트에서 제거
        gameModel.RemoveEquipmentList(item);
        // 장착중인 장비 리스트에 추가
        gameModel.AddEquippedItemList(equippedItem);
        // 장비 스텟 추가

        // 저장
        gameModel.SaveGameData();
    }
}
