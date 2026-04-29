using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] private Transform _selectionParent;

    private UpgradeSelectionView _selectPrefab;

    private IUpgradable[] _upgradables;

    private List<UpgradeSelectionView> _views = new List<UpgradeSelectionView>();

    public event UnityAction<bool> SetUpgradeActive;

    private void Awake()
    {
        _upgradables = GetComponentsInChildren<IUpgradable>(true);

        Debug.Log(_upgradables.Length);
    }

    private void Start()
    {
        //TODO 추후 GamaManager가 StartScene에서 생성될 시 Start > Awake로 수정
        _selectPrefab = GameManager.Instance.ResourceManager.GetPrefabOnce<UpgradeSelectionView>("Prefabs/Ability");
    }

    public void OnLevelUp(int level)
    {
        if (level <= 0)
            return;

        SelectStart();
    }

    //TODO 더 이상 업그레이드 가능한 것이 없을 경우 어떻게 할 지 생각
    private void SelectStart()
    {
        // 게임 진행속도 0
        Time.timeScale = 0.0f;

        // 업그레이드 패널 활성화
        SetUpgradeActive?.Invoke(true);

        foreach(IUpgradable x in _upgradables)
        {
            if (x.Level < 5)
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
    
    private void EndSelect()
    {
        // 게임 진행속도 1
        Time.timeScale = 1.0f;

        foreach(var views in _views)
        {
            Destroy(views.gameObject);
        }

        _views.Clear();

        // 업그레이드 패널 비활성화
        SetUpgradeActive?.Invoke(false);
    }
}
