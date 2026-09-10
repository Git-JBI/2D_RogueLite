using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaItemPresenter : MonoBehaviour
{
    private EquipmentItemDatabase _equipmentItemDatabase;
    private ItemViewer[] _viewers;
    private Button _button;

    private void Awake()
    {
        _equipmentItemDatabase = GameManager.Instance.EquipmentItemDatabase;

        _button = GetComponent<Button>();
        _viewers = GetComponentsInChildren<ItemViewer>();

        foreach (ItemViewer x in _viewers)
        {
            x.gameObject.SetActive(false);
        }
        _button.onClick.AddListener(ClosePanel);
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void ShowOne(string id, ItemRarity rarity)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        foreach (ItemViewer x in _viewers)
        {
            x.gameObject.SetActive(false);
        }

        _viewers[0].gameObject.SetActive(true);

        Sprite itemSprite = _equipmentItemDatabase.GetEquipmentItemSprite(id);
        Sprite raritySprite = _equipmentItemDatabase.GetRaritySprite(rarity);

        _viewers[0].Init(itemSprite, raritySprite);

        _viewers[0].gameObject.transform.localScale = Vector3.one * 1.2f;
        _viewers[0].gameObject.transform.DOScale(1, 0.2f);
    }

    public void ShowTen(List<EquipmentItemData> datas)
    {
        gameObject.SetActive(true);

        StartCoroutine(ShowRoutine(datas, 10));
    }

    private IEnumerator ShowRoutine(List<EquipmentItemData> datas, int count)
    {
        foreach (ItemViewer x in _viewers)
        {
            x.gameObject.SetActive(false);
        }

        for (int i = 0; i < count; i++)
        {
            _viewers[i].gameObject.SetActive(true);

            Sprite itemSprite = _equipmentItemDatabase.GetEquipmentItemSprite(datas[i].ItemID);
            Sprite raritySprite = _equipmentItemDatabase.GetRaritySprite(datas[i].Rarity);

            _viewers[i].Init(itemSprite, raritySprite);
            _viewers[i].gameObject.transform.localScale = Vector3.one * 1.2f;
            _viewers[i].gameObject.transform.DOScale(1, 0.2f);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
