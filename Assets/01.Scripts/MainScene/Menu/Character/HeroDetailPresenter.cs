using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class HeroDetailPresenter : MonoBehaviour
{
    [SerializeField] private HeroDetailViewer _viewer;
    [SerializeField] private Transform _slotTransform;
    private HeroDetailModel _model = new();
    private HeroSlotPresenter _slot;
    private HeroModel _heroModel;

    private List<HeroSlotPresenter> _slots = new();

    public event Action<AnimatorController> OnDetailClosed;

    public void Awake()
    {
        _slot = Resources.Load<HeroSlotPresenter>("Prefabs/Slots/HeroSlot");

        _heroModel = GameManager.Instance.GameModel.HeroModel;

        _viewer.OnBackButtonClicked += CloseDetailPanel;

        Init();

        for(int i = 0; i < _heroModel.GetHeroCount(); i++)
        {
            HeroSlotPresenter slot = Instantiate(_slot, _slotTransform);
            string slotHeroId = i.ToString("D2");
            bool isSelected = _heroModel.GetHeroId() == slotHeroId;

            slot.Init(i.ToString("D2"), isSelected, _heroModel.GetHeroPortraitSprite(slotHeroId));

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
        // HeroUIAnimController 설정
        _viewer.SetAnimController(_heroModel.GetHeroUIAnimController());

        // HeroName 설정
        _viewer.SetHeroNameText(_heroModel.GetHeroNameToString());

        // HeroDesc 설정
        _viewer.SetHeroDescText(_heroModel.GetDesc());
    }

    private void CloseDetailPanel()
    {
        OnDetailClosed?.Invoke(_heroModel.GetHeroUIAnimController());
    }
}
