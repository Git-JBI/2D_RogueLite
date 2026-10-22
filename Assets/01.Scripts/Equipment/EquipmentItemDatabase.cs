using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentItemDatabase : MonoBehaviour
{
    /// <summary>
    /// 레어리티에 따른 스프라이트 데이터
    /// </summary>
    private SO_RaritySprite _raritySprite;
    /// <summary>
    /// 아이템 타입에 따른 스프라이트 데이터
    /// </summary>
    private SO_ItemTypeSpriteData _itemTypeSpriteData;
    /// <summary>
    /// 장비 아이템 리스트
    /// </summary>
    private List<SO_EquipmentItemData> _equipmentItems;

    /// <summary>
    /// 사각형 레어리티 스프라이트 딕셔너리
    /// </summary>
    private Dictionary<ItemRarity, Sprite> _squareRaritySpriteMap = new();
    /// <summary>
    /// 원형 레어리티 스프라이트 딕셔너리
    /// </summary>
    private Dictionary<ItemRarity, Sprite> _circleRaritySpriteMap = new();
    private Dictionary<ItemRarity, Sprite> _diamondRaritySpriteMap = new();
    private Dictionary<EquipmentType, Sprite> _itemTypeSpriteMap = new();
    /// <summary>
    /// 장비 아이템 딕셔너리
    /// </summary>
    private Dictionary<string, SO_EquipmentItemData> _equipmentItemMap = new();

    public IReadOnlyList<SO_EquipmentItemData> EquipmentItemDatas => _equipmentItems;

    private void Awake()
    {
        // 레어리티 스프라이트 데이터 불러오기
        _raritySprite = Resources.Load<SO_RaritySprite>("GameDatas/RaritySpriteData/RaritySpriteData");
        // 아이템 타입 스프라이트 데이터 불러오기
        _itemTypeSpriteData = Resources.Load<SO_ItemTypeSpriteData>("GameDatas/ItemTypeSpriteData/ItemTypeSpriteData");

        // 레어리티 스프라이트 딕셔너리에 레어리티 스프라이트 데이터 추가
        for(int i = 0; i < _raritySprite.SquareSprites.Length; i++)
        {
            ItemRarity rarity = (ItemRarity)i;

            _squareRaritySpriteMap.Add(rarity, _raritySprite.SquareSprites[i]);
            _circleRaritySpriteMap.Add(rarity, _raritySprite.CircleSprites[i]);
            _diamondRaritySpriteMap.Add(rarity, _raritySprite.DiamondSprites[i]);
        }

        // 아이템 타입 스프라이트 딕셔너리에 데이터 추가
        for(int i = 0; i < 6; i++)
        {
            EquipmentType type = (EquipmentType)i;

            _itemTypeSpriteMap.Add(type, _itemTypeSpriteData.ItemTypeSprites[i]);
        }

        // 장비 아이템데이터 Resources.LoadAll로 전부 불러오기
        _equipmentItems = Resources.LoadAll<SO_EquipmentItemData>("GameDatas/ItemData").ToList();

        // 장비 아이템 딕셔너리에 장비 아이템 데이터 추가
        foreach(SO_EquipmentItemData item in _equipmentItems)
        {
            if (item == null)
                continue;

            if(_equipmentItemMap.ContainsKey(item.Id))
            {
                Debug.Log($"중복된 아이템 {item.Id}가 있습니다.");
                continue;
            }
            _equipmentItemMap.Add(item.Id, item);
        }
    }

    public SO_EquipmentItemData GetEquipmentItemData(string itemId)
    {
        if(_equipmentItemMap.TryGetValue(itemId, out SO_EquipmentItemData itemData))
            return itemData;

        Debug.LogWarning($"{itemId}가 없습니다.");
        return null;
    }

    public Sprite GetSquareRaritySprite(ItemRarity rarity)
    {
        if (_squareRaritySpriteMap.TryGetValue(rarity, out Sprite sprite))
            return sprite;

        Debug.LogWarning($"{rarity}의 스프라이트가 없습니다.");
        return null;
    }
    public Sprite GetCircleRaritySprite(ItemRarity rarity)
    {
        if (_circleRaritySpriteMap.TryGetValue(rarity, out Sprite sprite))
            return sprite;

        Debug.LogWarning($"{rarity}의 스프라이트가 없습니다.");
        return null;
    }
    public Sprite GetDiamondRaritySprite(ItemRarity rarity)
    {
        if (_diamondRaritySpriteMap.TryGetValue(rarity, out Sprite sprite))
            return sprite;

        Debug.LogWarning($"{rarity}의 스프라이트가 없습니다.");
        return null;
    }
    public Sprite GetItemTypeSprite(EquipmentType type)
    {
        if (_itemTypeSpriteMap.TryGetValue(type, out Sprite sprite))
            return sprite;

        Debug.LogWarning($"{type}의 스프라이트가 없습니다.");
        return null;
    }

    public string GetEquipmentItemDesc(string itemId)
    {
        SO_EquipmentItemData itemData = GetEquipmentItemData(itemId);

        if (itemData == null)
            return "";

        return itemData.Desc;
    }

    public string GetEquipmentItemName(string itemId)
    {
        SO_EquipmentItemData itemData = GetEquipmentItemData(itemId);

        if (itemData == null)
            return "";

        return itemData.ItemName;
    }

    public Sprite GetEquipmentItemSprite(string itemId, ItemRarity rarity)
    {
        SO_EquipmentItemData itemData = GetEquipmentItemData(itemId);

        if (itemData == null)
            return null;

        // Unique 등급 이상이라면
        if ((int)rarity >= 3)
            return itemData.IconSprites[1];
        else
            return itemData.IconSprites[0];
    }
}
