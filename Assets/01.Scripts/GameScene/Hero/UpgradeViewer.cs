using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeViewer : MonoBehaviour, IUpgradeViewer
{
    private UpgradeSelection[] _selections;

    private void Awake()
    {
        _selections = GetComponentsInChildren<UpgradeSelection>(true);
    }

    #region Interface
    public event UnityAction OnSelected;

    public void Hide()
    {
        foreach(UpgradeSelection view in _selections)
        {
            // selectionView 숨기기
            view.Hide();
        }

        // 이벤트 실행
        OnSelected?.Invoke();

        // 이벤트 비움
        OnSelected = null;

        // 뷰어 비활성화
        gameObject.SetActive(false);
    }

    public void Show(List<IUpgradable> upgradables)
    {
        // 뷰어 활성화
        gameObject.SetActive(true);

        for(int i = 0; i < upgradables.Count; i++)
        {
            // selection 활성화
            _selections[i].gameObject.SetActive(true);

            // selection 초기화
            _selections[i].Init(upgradables[i]);

            // selection의 OnSelect 이벤트에 Hide()추가
            _selections[i].OnSelected += Hide;
        }
    }
    #endregion
}