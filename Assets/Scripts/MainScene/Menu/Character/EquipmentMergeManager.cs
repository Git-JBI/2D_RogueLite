using UnityEngine;

public class EquipmentMergeManager : MonoBehaviour
{
    // Common + Commom + Common = UnCommon (동일 아이템ID)
    // UnCommon + UnCommon + UnCommon = Rare (동일 아이템ID)
    // Rare + Rare + Rare = Unique (동일 아이템ID)
    // Unique + Unique = Unique+1 (동일 아이템 타입)
    // Unique+1 + Unique + Unique = Unique+2 (동일 아이템 타입)
    // Unique+2 + Unique+2 = Epic (동일 아이템ID)
    // Epic + Epic = Epic+1 (동일 아이템 타입)
    // Epic+1 + Epic = Epic+2 (동일 아이템 타입)
    // Epic+2 + Epic = Epic+3 (동일 아이템 타입)
    // Epic+3 + Epic + Epic = Legendary (동일 아이템ID)
    // Legendary + Legendary = Legendary+1 (동일 아이템 타입)
    // Legendary+1 + Legendary = Legendary+2 (동일 아이템 타입)
    // Legendary+2 + Legendary = Legendary+3 (동일 아이템 타입)
    // Legendary+3 + Legendary = Legendary+4 (동일 아이템 타입)
}
