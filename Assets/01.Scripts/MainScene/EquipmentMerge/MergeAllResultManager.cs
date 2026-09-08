using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MergeAllResultManager : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _closeButton;
    private EquipmentItemSlot _itemSlot;
    private Coroutine _showRoutine;

    private List<EquipmentItemSlot> _slots = new();

    private void Awake()
    {
        //TODO 생성할 슬롯(?) -> 풀링으로 변경
        _itemSlot = Resources.Load<EquipmentItemSlot>("Prefabs/Slots/ItemSlot");

        // 외각 터치 시 패널 닫는 이벤트 추가
        _closeButton.onClick.AddListener(ClosePanel);
    }

    public void ShowResult(List<EquipmentItemData> items)
    {
        if(_showRoutine != null)
        {
            StopCoroutine(_showRoutine);
            _showRoutine = null;
        }

        // 받아온 아이템들 갯수가 현재 슬롯 갯수보다 많을 경우
        if(_slots.Count < items.Count)
        {
            int count = items.Count - _slots.Count;
            // 슬롯 생성 > _slots에 추가 > 비활성화
            for(int i = 0; i < count; i++)
            {
                EquipmentItemSlot slot = Instantiate(_itemSlot, _content);
                _slots.Add(slot);
                slot.gameObject.SetActive(false);
            }
        }
        _showRoutine = StartCoroutine(ShowCoroutine(items));
    }

    private IEnumerator ShowCoroutine(List<EquipmentItemData> items)
    {
        // 아이템 리스트 정렬
        List<EquipmentItemData> itemList = items.OrderByDescending(i => i.Rarity).ToList();

        // 받아온 아이템들 슬롯에 적용
        for (int i = 0; i < itemList.Count; i++)
        {
            // 슬롯 활성화
            _slots[i].gameObject.SetActive(true);
            // 슬롯 초기화
            _slots[i].Init(itemList[i]);
            // 슬롯 스케일 변경(1.2)
            _slots[i].gameObject.transform.localScale = Vector3.one * 1.2f;
            // 슬롯 스케일 원상복귀(1.0 / 0.2초 동안)
            _slots[i].gameObject.transform.DOScale(Vector3.one, 0.2f);
            // 0.2초 대기
            yield return new WaitForSeconds(0.2f);
        }

        // 텍스트 활성화
        _text.gameObject.SetActive(true);

        // 루틴 비우기
        _showRoutine = null;
    }

    private void ClosePanel()
    {
        if (_showRoutine == null)
        {
            // 슬롯들 비활성화
            foreach (EquipmentItemSlot x in _slots)
            {
                x.gameObject.SetActive(false);
            }

            // 텍스트 비활성화
            _text.gameObject.SetActive(false);

            // 패널 끄기
            gameObject.SetActive(false);
        }
    }
}