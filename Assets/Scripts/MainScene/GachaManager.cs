using System.Collections.Generic;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    //TODO 여기서 불러오는게 맞을까?
    [SerializeField] private List<SO_EquipmentItemData> _totalItemDataPool;

    private Button_Gacha[] _button_Gachas;

    //TODO 추후 UI쪽에서 받아오기
    [SerializeField] private GetItemViewer _itemViewer;

    private void Awake()
    {
        _button_Gachas = GetComponentsInChildren<Button_Gacha>();

        foreach(Button_Gacha x in _button_Gachas)
        {
            x.Init();
        }

        _button_Gachas[0].OnButtonClicked += GachaEquipmentItemOne;
        _button_Gachas[1].OnButtonClicked += GachaEquipmentItemTen;
    }
    /*
     * 각 종류별 뽑기에 대한 등급별 확률로 등급 결정
       ->
       설정 데이터 딕셔너리에서 뽑힌 등급에 해당하는 아이템 아이디 리스트
       ->
       그중에서 랜덤하게 하나
     */

    /// <summary>
    /// 단일 장비 뽑기
    /// </summary>
    public EquipmentItemData PickEquipmentItemOne()
    {
        // 아이템 티어 결정
        ItemTier itemTier = DetermineTier();

        // 아이템 레어리티 결정
        ItemRarity itemRarity = DetermineRarity();

        // 아이템 데이터 내에서 뽑기 타입에 따른 아이템 필터링기능 (현재는 필터링 필요 없음)
        // List<SO_EquipmentItemData> filteredPool = _totalItemDataPool.FindAll(item => item.itemType == ItemType.Equipment);
        List<SO_EquipmentItemData> filteredPool = _totalItemDataPool;

        // 데이터가 존재하지 않을 경우
        if(filteredPool == null || filteredPool.Count == 0)
        {
            Debug.Log("filteredPool에 아무것도 존재하지 않습니다.");
            return null;
        }

        // 필터링 장비중 아무거나 선택
        SO_EquipmentItemData pickedItem = filteredPool[Random.Range(0, filteredPool.Count)];

        // 저장할 데이터 생성
        EquipmentItemData saveEquipItem = new EquipmentItemData
        {
            InstanceID = System.Guid.NewGuid().ToString(),
            ItemID = pickedItem.Id,
            Rarity = itemRarity,
            RarityLevel = 0,
            ItemLevel = 1,
            Desc = pickedItem.Desc,
        };

        return saveEquipItem;
    }

    public List<EquipmentItemData> PickEquipmentItemTen()
    {
        List<EquipmentItemData> saveEquipItems = new();
        for (int i = 0; i < 10; i++)
        {
            EquipmentItemData saveEquipItem = PickEquipmentItemOne();
            if (saveEquipItem != null)
            {
                saveEquipItems.Add(saveEquipItem);
            }
        }
        return saveEquipItems;
        // 획득한 아이템 보여주기
        // _itemViewer.ShowTen(itemKeys);
    }

    public void GachaEquipmentItemOne()
    {
        // 금액 확인
        // 없을 시 불가
        // 있을 시 실행
        EquipmentItemData equipmentItemData = PickEquipmentItemOne();

        // 실행 했다면 뽑은 아이템 저장
        GameManager.Instance.GameModel.GetEquipItem(equipmentItemData);

        // 획득한 아이템 보여주기
        _itemViewer.ShowOne(equipmentItemData);
    }

    public void GachaEquipmentItemTen()
    {
        // 금액 확인
        // 없을 시 불가
        // 있을 시 실행
        List<EquipmentItemData> equipmentItemDatas = PickEquipmentItemTen();

        // 실행 했다면 뽑은 아이템 저장
        for(int i = 0; i < equipmentItemDatas.Count; i++)
        {
            GameManager.Instance.GameModel.GetEquipItem(equipmentItemDatas[i]);
        }

        // 획득한 아이템 보여주기
        _itemViewer.ShowTen(equipmentItemDatas);
    }
    

    #region 가중치 계산 함수
    /// <summary>
    /// 아이템의 등급을 결정하는 함수
    /// </summary>
    private ItemRarity DetermineRarity()
    {
        int[] rarityWeights = { 70, 20, 8, 2 };
        int totalWeight = 0;

        foreach(int w in rarityWeights)
            totalWeight += w;

        int randomValue = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        for(int i = 0; i < rarityWeights.Length; i++)
        {
            cumulativeWeight += rarityWeights[i];
            if(randomValue < cumulativeWeight)
            {
                return (ItemRarity)i;
            }
        }
        return ItemRarity.Common;
    }

    /// <summary>
    /// 아이템의 티어를 결정하는 함수
    /// </summary>
    /// <returns></returns>
    private ItemTier DetermineTier()
    {
        int[] tierWeights = { 90, 9, 1 };
        int totalWeight = 0;
        foreach(int w in tierWeights)
            totalWeight += w;

        int randomValue = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        for(int i = 0; i < tierWeights.Length; i++)
        {
            cumulativeWeight += tierWeights[i];
            if(randomValue < cumulativeWeight)
            {
                return (ItemTier)i; // 0.Default, 1. SClass, 2. SSClass
            }
        }
        return ItemTier.Default;
    }
    #endregion
}