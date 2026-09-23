using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaItemPresenter : MonoBehaviour
{
    private EquipmentItemDatabase _equipmentItemDatabase;
    private GachaItemViewer[] _viewers;
    [SerializeField] private Button _backgroundButton;
    [SerializeField] private TMP_Text _closeText;

    private bool _isSpecial;

    private void Awake()
    {
        _equipmentItemDatabase = GameManager.Instance.EquipmentItemDatabase;

        _viewers = GetComponentsInChildren<GachaItemViewer>();

        foreach (GachaItemViewer x in _viewers)
        {
            x.gameObject.SetActive(false);
        }
    }

    private void ClosePanel()
    {
        if(_isSpecial)
        {
            _isSpecial = false;
            foreach(GachaItemViewer viewer in _viewers)
            {
                viewer.TurnOffEffect();
            }
        }
        _closeText.gameObject.SetActive(false);
        _backgroundButton.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
    }

    public void ShowOne(string id, ItemRarity rarity)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        foreach (GachaItemViewer x in _viewers)
        {
            x.gameObject.SetActive(false);
        }

        _viewers[0].gameObject.SetActive(true);

        Sprite itemSprite = _equipmentItemDatabase.GetEquipmentItemSprite(id, rarity);
        Sprite raritySprite = _equipmentItemDatabase.GetSquareRaritySprite(rarity);

        _viewers[0].SetItemSprite(itemSprite);
        _viewers[0].SetRaritySprite(raritySprite);
        if (rarity == ItemRarity.Unique)
        {
            _isSpecial = true;
            _viewers[0].GetSpecialItem();
        }

        _viewers[0].gameObject.transform.localScale = Vector3.one * 1.2f;
        _viewers[0].gameObject.transform.DOScale(1, 0.2f);

        _closeText.gameObject.SetActive(true);
        _backgroundButton.onClick.AddListener(ClosePanel);
    }

    public void ShowTen(List<EquipmentItemData> datas)
    {
        gameObject.SetActive(true);

        StartCoroutine(ShowRoutine(datas, 10));
    }

    private IEnumerator ShowRoutine(List<EquipmentItemData> datas, int count)
    {
        foreach (GachaItemViewer x in _viewers)
        {
            x.gameObject.SetActive(false);
        }

        for (int i = 0; i < count; i++)
        {
            _viewers[i].gameObject.SetActive(true);

            Sprite itemSprite = _equipmentItemDatabase.GetEquipmentItemSprite(datas[i].ItemID, datas[i].Rarity);
            Sprite raritySprite = _equipmentItemDatabase.GetSquareRaritySprite(datas[i].Rarity);

            _viewers[i].SetItemSprite(itemSprite);
            _viewers[i].SetRaritySprite(raritySprite);
            if (datas[i].Rarity == ItemRarity.Unique)
            {
                _isSpecial = true;
                _viewers[i].GetSpecialItem();
            }

            _viewers[i].gameObject.transform.localScale = Vector3.one * 1.2f;
            _viewers[i].gameObject.transform.DOScale(1, 0.2f);
            yield return new WaitForSeconds(0.2f);
        }

        _closeText.gameObject.SetActive(true);
        _backgroundButton.onClick.AddListener(ClosePanel);
    }
}
