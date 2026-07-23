using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    /// <summary>
    /// 레벨업 시 선택지를 보여줄 업그레이드 뷰어
    /// </summary>
    private IUpgradeViewer _iUpgradeViewer;
    /// <summary>
    /// 업그레이드 가능한 목록들
    /// </summary>
    private List<IUpgradable> _upgradables = new();
    /// <summary>
    /// 각성에 필요한 내용 설정 데이터
    /// </summary>
    private List<SO_AwakeningConfigData> _awakeningDatas = new();
    private WeaponController _weaponController;
    private PassiveController _passiveController;

    private Dictionary<WeaponType, WeaponBase> _weaponMap = new();
    private Dictionary<PassiveType, PassiveBase> _passiveMap = new();
    private Dictionary<AwakeningWeaponType, WeaponBase> _awakeningWeaponMap = new();

    private void Awake()
    {
        _awakeningDatas = Resources.LoadAll<SO_AwakeningConfigData>("GameDatas/WeaponAwakeningData").ToList();
    }

    public void Init(IUpgradeViewer viewer, WeaponController wc, PassiveController pc)
    {
        _iUpgradeViewer = viewer;
        _weaponController = wc;
        _passiveController = pc;

        foreach(WeaponBase x in _weaponController.Weapons)
        {
            Debug.Log(x.WeaponName);
            _upgradables.Add(x);
        }
        foreach(PassiveBase x in _passiveController.Passives)
        {
            Debug.Log(x.PassiveName);
            _upgradables.Add(x);
        }
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
        // 게임 진행속도 0 설정
        Time.timeScale = 0.0f;

        // Viewer에 전해줄 업그레이드들
        List<IUpgradable> upgradables = new();

        //TODO 현재 무기만 가능
        foreach(SO_AwakeningConfigData x in _awakeningDatas)
        {

        }


        // // 각성 가능 여부 체크(최대 레벨이 6이라면)
        // foreach (WeaponBase weapon in _weapons)
        // {
        //     // 최대 레벨이 6이며(각성 가능한 무기 체크) 현재 레벨이 5인 무기일 경우
        //     if (weapon.MaxLevel == 6 && weapon.Level == 5)
        //     {
        //         // 각성 설정 데이터 찾기
        //         SO_AwakeningConfigData data = _awakeningDatas.Find(i => i.BaseWeapon == weapon.WeaponName);
        //         if (data != null)
        //         {
        //             // 각성에 필요한 패시브가 있는지 여부 판단
        //             PassiveBase requirePassive = _passives.Find(i => i.PassiveName == data.Passive);
        //             if (requirePassive != null && requirePassive.Level >= 1)
        //             {
        //                 upgradables.Add(weapon);
        //                 if (upgradables.Count == 3)
        //                     break;
        //             }
        //         }
        //     }
        // }

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