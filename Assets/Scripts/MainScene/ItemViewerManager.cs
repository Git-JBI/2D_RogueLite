using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemViewerManager : MonoBehaviour, IPointerClickHandler
{
    private ItemViewer[] _viewers;

    private void Awake()
    {
        _viewers = GetComponentsInChildren<ItemViewer>();

        foreach (ItemViewer x in _viewers)
        {
            x.gameObject.SetActive(false);
        }
    
        gameObject.SetActive(false);
    }

    public void ShowOne(EquipmentItemData data)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        _viewers[0].gameObject.SetActive(true);

        ItemRarity rarity = data.Rarity;
        string dataDesc = GameManager.Instance.EquipmentItemDatabase.GetEquipmentItemDesc(data.ItemID);

        _viewers[0].Init(rarity, dataDesc);
    }

    public void ShowTen(List<EquipmentItemData> datas)
    {
        gameObject.SetActive(true);

        for(int i = 0; i < _viewers.Length; i++)
        {
            _viewers[i].gameObject.SetActive(true);

            ItemRarity rarity = datas[i].Rarity;
            string dataDesc = GameManager.Instance.EquipmentItemDatabase.GetEquipmentItemDesc(datas[i].ItemID);

            _viewers[i].Init(rarity, dataDesc);
        }
    }

    private void Close()
    {
        for(int i = 0; i < _viewers.Length; i++)
        {
            _viewers[i].gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Close();
    }
}
