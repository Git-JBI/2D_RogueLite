using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    private IUpgradeViewer _iUpgradeViewer;
    private List<IUpgradable> _upgradables = new();

    private void Awake()
    {
        _upgradables = GetComponentsInChildren<IUpgradable>(true).ToList();
    }

    public void Init(IUpgradeViewer viewer)
    {
        _iUpgradeViewer = viewer;
    }

    public void OnLevelUp(int level)
    {
        if (level <= 0)
            return;

        LevelUp();
    }
    /*
     * 무기 조합에 대한 설정 데이터 추가
     * 설정 데이터에 따른 
     */

    private void LevelUp()
    {
        // Viewer에 전해줄 업그레이드들
        List<IUpgradable> upgradables = new();

        // 게임 진행속도 0 설정
        Time.timeScale = 0.0f;

        // _upgradables 랜덤으로 섞기
        _upgradables.Shuffle();

        // _upgradables에서 3 종류 선택
        foreach(IUpgradable x in _upgradables)
        {
            // upgradabels의 숫자가 3일 경우
            if (upgradables.Count == 3)
                break;
            //TODO Level체크를 5 값을 주는게 아니여도 될듯
            // Level이 5 미만 && 영구적 능력일 경우
            if(x.Level < 5 && x.IsPermanentAbility)
            {
                upgradables.Add(x);
            }
        }

        // 뽑을 경우가 없을 경우(IsPermanentAbility가 False인 경우)
        if (upgradables.Count == 0)
        {
            foreach(IUpgradable x in _upgradables)
            {
                if(!x.IsPermanentAbility)
                {
                    upgradables.Add(x);
                }
            }
        }

        _iUpgradeViewer.Show(upgradables);

        _iUpgradeViewer.OnSelected += EndSelect;
    }
    
    private void EndSelect()
    {
        // 게임 진행속도 1
        Time.timeScale = 1.0f;
    }
}