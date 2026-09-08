using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeResultViewer : MonoBehaviour
{
    [SerializeField] private Button _background;
    [SerializeField] private EquipmentItemSlot _mainItemSlot;
    [SerializeField] private EquipmentItemSlot _oneRequireSlot;
    [SerializeField] private EquipmentItemSlot _twoRequireSlot01;
    [SerializeField] private EquipmentItemSlot _twoRequireSlot02;
    [SerializeField] private GameObject _explainText;

    private MergeRule _upgradeData;
    private List<Vector3> _requireSlotPos = new();
    // 합성에 쓰인 재료가 1개인지 2개인지
    // 메인슬롯, 합성재료슬롯(3개{1개 / 2개})

    public void Init(EquipmentItemData main, MergeRule upgradeData, EquipmentItemData one)
    {
        // 메인 재료 초기화
        _mainItemSlot.Init(main);

        // 업그레이드 데이터
        _upgradeData = upgradeData;

        // 재료 슬롯 활성화
        _oneRequireSlot.gameObject.SetActive(true);

        // 최초 위치 저장
        _requireSlotPos.Add(_oneRequireSlot.transform.position);

        // 재료 슬롯 초기화
        _oneRequireSlot.Init(one);

        // 코루틴 실행
        StartCoroutine(RequireOneRoutine());
    }

    public void Init(EquipmentItemData main, MergeRule upgradeData, EquipmentItemData two1, EquipmentItemData two2)
    {
        // 메인 재료 초기화
        _mainItemSlot.Init(main);

        // 업그레이드 데이터
        _upgradeData = upgradeData;

        // 재료 슬롯 활성화
        _twoRequireSlot01.gameObject.SetActive(true);
        _twoRequireSlot02.gameObject.SetActive(true);

        // 최초 위치 저장
        _requireSlotPos.Add(_twoRequireSlot01.transform.position);
        _requireSlotPos.Add(_twoRequireSlot02.transform.position);

        // 재료 슬롯 초기화
        _twoRequireSlot01.Init(two1);
        _twoRequireSlot02.Init(two2);

        // 코루틴 실행
        StartCoroutine(RequireTwoRoutine());
    }

    private IEnumerator RequireOneRoutine()
    {
        // 0.5초 대기
        yield return new WaitForSeconds(0.5f);

        // 1초간 필요 아이템 슬롯 메인 슬롯으로 움직임
        _oneRequireSlot.transform.DOMove(_mainItemSlot.transform.position, 1);

        // 1초 대기
        yield return new WaitForSeconds(1);

        // 필요 아이템 위치 초기화
        _oneRequireSlot.transform.position = _requireSlotPos[0];

        // 필요 아이템 슬롯 비활성화
        _oneRequireSlot.gameObject.SetActive(false);

        // 메인 슬롯을 초기화할 업그레이드 데이터
        EquipmentItemData data = _mainItemSlot.ItemData;

        data.Rarity = _upgradeData.UpgradedRarity;
        data.RarityLevel = _upgradeData.UpgradedRarityLevel;

        // 메인 슬롯 초기화
        _mainItemSlot.Init(data);

        // 탭하여 닫기 텍스트 활성화
        _explainText.SetActive(true);

        // 뒷 배경 버튼에 누르면 창 닫히는 함수 추가
        _background.onClick.AddListener(CloseResultPanel);
    }
    private IEnumerator RequireTwoRoutine()
    {
        // 0.5초 대기
        yield return new WaitForSeconds(0.5f);

        // 1초간 필요 아이템 슬롯 메인 슬롯으로 움직임
        _twoRequireSlot01.transform.DOMove(_mainItemSlot.transform.position, 1);
        _twoRequireSlot02.transform.DOMove(_mainItemSlot.transform.position, 1);

        // 1초 대기
        yield return new WaitForSeconds(1);

        // 필요 아이템 위치 초기화
        _twoRequireSlot01.transform.position = _requireSlotPos[0];
        _twoRequireSlot02.transform.position = _requireSlotPos[1];

        // 필요 아이템 슬롯 비활성화
        _twoRequireSlot01.gameObject.SetActive(false);
        _twoRequireSlot02.gameObject.SetActive(false);

        // 메인 슬롯을 초기화할 업그레이드 데이터
        EquipmentItemData data = _mainItemSlot.ItemData;

        data.Rarity = _upgradeData.UpgradedRarity;
        data.RarityLevel = _upgradeData.UpgradedRarityLevel;

        // 메인 슬롯 초기화
        _mainItemSlot.Init(data);

        // 탭하여 닫기 텍스트 활성화
        _explainText.SetActive(true);

        // 뒷 배경 버튼에 누르면 창 닫히는 함수 추가
        _background.onClick.AddListener(CloseResultPanel);
    }

    /// <summary>
    /// 패널 닫을 시 불러올 함수
    /// </summary>
    private void CloseResultPanel()
    {
        // 배경 이벤트 제거
        _background.onClick.RemoveAllListeners();

        // 위치 리스트 제거
        _requireSlotPos.Clear();

        // 탭하여 닫기 텍스트 비활성화
        _explainText.SetActive(false);

        // 패널 비활성화
        gameObject.SetActive(false);
    }
}