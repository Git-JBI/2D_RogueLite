using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] private Transform _selectionParent;

    private UpgradeSelectionView _selectPrefab;

    private List<UpgradeSelectionView> _views = new List<UpgradeSelectionView>();

    public event UnityAction<bool> SetUpgradeActive;

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

    private void SelectStart()
    {
        // 게임 진행속도 0
        Time.timeScale = 0.0f;

        // 업그레이드 패널 활성화
        SetUpgradeActive?.Invoke(true);

        UpgradeSelectionView view = Instantiate(_selectPrefab, _selectionParent);

        //TODO 업그레이드 선택지 IUpgradable
        // view.Init();

        // 리스트 추가
        _views.Add(view);

        view.OnSelect += EndSelect;
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
