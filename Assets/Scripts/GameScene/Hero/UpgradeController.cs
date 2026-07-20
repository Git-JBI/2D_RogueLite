using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] private Transform _selectionParent;

    private UpgradeSelectionView _selectPrefab;

    private List<IUpgradable> _upgradables = new();

    private List<UpgradeSelectionView> _views = new List<UpgradeSelectionView>();

    public event UnityAction<bool> OnUpgradeActive;

    private void Awake()
    {
        _upgradables = GetComponentsInChildren<IUpgradable>(true).ToList();

        _selectPrefab = Resources.Load<UpgradeSelectionView>("Prefabs/Ability");
    }

    public void OnLevelUp(int level)
    {
        if (level <= 0)
            return;

        LevelUp();
    }

    private void LevelUp()
    {
        // 게임 진행속도 0
        Time.timeScale = 0.0f;

        // 업그레이드 패널 활성화
        OnUpgradeActive?.Invoke(true);

        //TODO 각성 가능여부 확인하기 (각성 여부 판단하는 룰을 만들어야 할 것 같음)
        // 3종 랜덤으로 뽑기
        // _upgradables 랜덤으로 섞기
        Util.Shuffle<IUpgradable>(_upgradables);

        foreach(IUpgradable x in _upgradables)
        {
            if (_views.Count == 3)
                return;
            // Level 5 미만일 경우(업그레이드 가능), IsPermanentAbility가 True인 경우
            if (x.Level < 5 && x.IsPermanentAbility)
            {
                UpgradeSelectionView view = Instantiate(_selectPrefab, _selectionParent);

                // 업그레이드 선택 뷰 초기화
                view.Init(x);

                // 선택 이벤트에 추가
                view.OnSelect += EndSelect;

                // 리스트 추가
                _views.Add(view);
            }
        }

        // 뽑을 경우가 없을 경우(IsPermanentAbility가 False인 경우)
        if (_views.Count == 0)
        {
            foreach(IUpgradable x in _upgradables)
            {
                if(!x.IsPermanentAbility)
                {
                    UpgradeSelectionView view = Instantiate(_selectPrefab, _selectionParent);

                    // 업그레이드 선택 뷰 초기화
                    view.Init(x);

                    // 선택 이벤트에 추가
                    view.OnSelect += EndSelect;

                    // 리스트 추가
                    _views.Add(view);
                }
            }
        }
    }
    
    private void EndSelect()
    {
        // 게임 진행속도 1
        Time.timeScale = 1.0f;

        foreach(var views in _views)
        {
            Destroy(views.gameObject);
        }

        // 뷰 리스트 비움
        _views.Clear();

        // 업그레이드 패널 비활성화
        OnUpgradeActive?.Invoke(false);
    }
}
