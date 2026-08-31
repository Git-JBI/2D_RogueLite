using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    /// <summary>
    /// 레벨업 시 선택지를 보여줄 업그레이드 뷰어
    /// </summary>
    private IUpgradeViewer _iUpgradeViewer;
    /// <summary>
    /// 업그레이드 가능한 리스트
    /// </summary>
    private List<IUpgradable> _upgradables = new();
    /// <summary>
    /// 무기 각성용 IUpgradable 리스트
    /// </summary>
    private List<IAwakeningUpgradable> _awakeningUpgradables = new();
    /// <summary>
    /// 각성에 필요한 데이터 리스트
    /// </summary>
    private List<SO_AwakeningConfigData> _awakeningDatas = new();

    private WeaponController _weaponController;
    private PassiveController _passiveController;

    private Dictionary<AwakeningWeaponType, IAwakeningUpgradable> _awakeningUpgradablesMap = new();

    private void Awake()
    {
        _awakeningDatas = Resources.LoadAll<SO_AwakeningConfigData>("GameDatas/WeaponAwakeningData").ToList();
    }

    public void Init(IUpgradeViewer viewer, WeaponController wc, PassiveController pc)
    {
        _iUpgradeViewer = viewer;
        _weaponController = wc;
        _passiveController = pc;

        foreach(IUpgradable x in _weaponController.Upgradables)
        {
            _upgradables.Add(x);
        }
        foreach(IUpgradable x in _passiveController.Upgradables)
        {
            _upgradables.Add(x);
        }
        foreach (SO_AwakeningConfigData data in _awakeningDatas)
        {
            IAwakeningUpgradable up = new AwakeningUpgradable(_weaponController, data);
            _awakeningUpgradables.Add(up);
            _awakeningUpgradablesMap.Add(data.ResultWeapon, up);
        }
    }

    public void OnLevelUp(int level)
    {
        if (level <= 0)
            return;

        LevelUp();
    }

    private void LevelUp()
    {
        // 게임 진행속도 0 설정
        Time.timeScale = 0.0f;

        // Viewer에 전해줄 업그레이드들
        List<IUpgradable> upgradables = new();

        //TODO 현재 무기만 가능
        // 무기 각성 데이터 순회
        foreach(SO_AwakeningConfigData x in _awakeningDatas)
        {
            IUpgradable weapon = _weaponController.GetIUpgradableWeapon(x.BaseWeapon);
            IUpgradable passive = _passiveController.GetIUpgradablePassive(x.Passive);

            // 무기가 5레벨이며 필요 패시브가 1레벨 이상인 경우
            if(weapon.Level == 5 && passive.Level >= 1)
            {
                IAwakeningUpgradable up = _awakeningUpgradablesMap[x.ResultWeapon];
                upgradables.Add(up);
            }
        }

        // _upgradables 랜덤으로 섞기
        _upgradables.Shuffle();

        // _upgradables에서 3 종류 선택
        foreach(IUpgradable x in _upgradables)
        {
            // upgradabels의 숫자가 3일 경우
            if (upgradables.Count == 3)
                break;

            // Level이 5 미만 && 영구적 능력일 경우
            if (x.Level < 5 && x.IsPermanentAbility)
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