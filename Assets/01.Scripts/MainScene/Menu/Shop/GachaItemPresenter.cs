using System.Collections.Generic;
using UnityEngine;

public class GachaItemPresenter : MonoBehaviour
{
    private EquipmentItemDatabase _equipmentItemDatabase;
    private ItemViewer[] _viewers;

    private void Awake()
    {
        _equipmentItemDatabase = GameManager.Instance.EquipmentItemDatabase;

        _viewers = GetComponentsInChildren<ItemViewer>();

        foreach (ItemViewer x in _viewers)
        {
            x.gameObject.SetActive(false);
        }
    
        gameObject.SetActive(false);
    }

    public void ShowOne(string id, ItemRarity rarity)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        _viewers[0].gameObject.SetActive(true);

        Sprite itemSprite = _equipmentItemDatabase.GetEquipmentItemSprite(id);
        Sprite raritySprite = _equipmentItemDatabase.GetRaritySprite(rarity);

        _viewers[0].Init(raritySprite, itemSprite);
    }

    public void ShowTen(List<EquipmentItemData> datas)
    {
        gameObject.SetActive(true);

        for(int i = 0; i < _viewers.Length; i++)
        {
            _viewers[i].gameObject.SetActive(true);

            Sprite itemSprite = _equipmentItemDatabase.GetEquipmentItemSprite(datas[i].ItemID);
            Sprite raritySprite = _equipmentItemDatabase.GetRaritySprite(datas[i].Rarity);

            _viewers[i].Init(itemSprite, raritySprite);
        }
    }
}
