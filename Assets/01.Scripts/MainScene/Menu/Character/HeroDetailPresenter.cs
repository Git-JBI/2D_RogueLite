using System;
using System.Collections.Generic;
using UnityEngine;

public class HeroDetailPresenter : MonoBehaviour
{
    [SerializeField] private HeroDetailViewer _viewer;
    [SerializeField] private Transform _slotTransform;

    private HeroDetailModel _model = new();
    private HeroModel _heroModel;
    private HeroSlotPresenter _slot;

    private List<HeroSlotPresenter> _slots = new();

    public event Action<RuntimeAnimatorController> OnDetailClosed;

    public void Awake()
    {
        _heroModel = GameManager.Instance.GameModel.HeroModel;
        _slot = Resources.Load<HeroSlotPresenter>("Prefabs/Slots/HeroSlot");

        _viewer.OnBackButtonClicked += CloseDetailPanel;
        _viewer.OnSelectButtonClicked += HandleSelectClicked;

        Init();

        for(int i = 0; i < _heroModel.GetHeroCount(); i++)
        {
            string slotHeroId = i.ToString("D2");
            bool isSelected = _heroModel.GetHeroId() == slotHeroId;

            HeroSlotPresenter slot = Instantiate(_slot, _slotTransform);

            if (!_heroModel.TryGetHeroConfigData(slotHeroId, out SO_HeroConfigData data))
                return;

            slot.Init(slotHeroId, isSelected, data.HeroPortraitSprite);
            slot.OnSlotClicked += HandleSlotClicked;
            _slots.Add(slot);

            if (isSelected)
            {
                _model.SetSelectedSlotId(slotHeroId);
                _model.SetChoosedSlotId(slotHeroId);
            }
        }
    }

    private void Init()
    {
        if (!_heroModel.TryGetHeroConfigData(_heroModel.GetHeroId(), out SO_HeroConfigData data))
            return;

        // HeroUIAnimController 설정
        _viewer.SetAnimController(data.HeroUIAnimController);

        // HeroName 설정
        _viewer.SetHeroNameText(data.HeroName.ToString());

        // HeroDesc 설정
        _viewer.SetHeroDescText(data.Desc);
    }
    private void HandleSlotClicked(string heroId)
    {
        // 설정 데이터를 불러오지 못했을 시 return
        if (!_heroModel.TryGetHeroConfigData(heroId, out SO_HeroConfigData data))
            return;

        // 현재 ChoosedSlotId와 heroId가 동일할 시 return
        if (_model.ChoosedSlotId == heroId)
            return;

        // choosedSlotId 설정
        _model.SetChoosedSlotId(heroId);

        // heroId == SlotHeroId가 같은 경우 선택된 슬롯 설정
        foreach (HeroSlotPresenter slot in _slots)
            slot.SetChoosed(slot.HeroId == heroId);

        // heroId == SelectedSlotId에 따른 선택 버튼 활성화 여부
        bool checkSelected = !(heroId == _model.SelectedSlotId);

        _viewer.SetActiveSelectButton(checkSelected);

        _viewer.SetAnimController(data.HeroUIAnimController);
        _viewer.SetHeroNameText(data.HeroName.ToString());
        _viewer.SetHeroDescText(data.Desc);
    }
    private void HandleSelectClicked()
    {
        // ChoosedSlotId 가져오기
        string choosedSlotId = _model.ChoosedSlotId;

        // SelectedSlot의 선택된 모습 비활성화
        int selectedSlotIndex = int.Parse(_model.SelectedSlotId);
        _slots[selectedSlotIndex].SetSelected(false);

        // 선택한 Hero 변경
        _heroModel.ChangeHero(choosedSlotId);
        // SelectedSlot 변경
        _model.SetSelectedSlotId(choosedSlotId);

        // ChoosedSlot을 SelectedSlot처리
        int slotIndex = int.Parse(choosedSlotId);
        _slots[slotIndex].SetSelected(true);
    }
    private void CloseDetailPanel()
    {
        if (!_heroModel.TryGetHeroConfigData(_model.SelectedSlotId, out SO_HeroConfigData data))
            return;

        OnDetailClosed?.Invoke(data.HeroUIAnimController);
    }
}
