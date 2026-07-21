using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    private IUpgradeViewer _iUpgradeViewer;
    private List<IUpgradable> _upgradables = new();
    private List<SO_WeaponAwakeningData> _awakeningDatas = new();
    private List<PassiveBase> _passives = new();
    private List<WeaponBase> _weapons = new();

    private void Awake()
    {
        _upgradables = GetComponentsInChildren<IUpgradable>(true).ToList();
        _awakeningDatas = Resources.LoadAll<SO_WeaponAwakeningData>("GameDatas/WeaponAwakeningData").ToList();
        _passives = GetComponentsInChildren<PassiveBase>(true).ToList();
        _weapons = GetComponentsInChildren<WeaponBase>(true).ToList();
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
        // 게임 진행속도 0 설정
        Time.timeScale = 0.0f;

        // Viewer에 전해줄 업그레이드들
        List<IUpgradable> upgradables = new();

        //TODO 현재 무기만 가능
        // 각성 가능 여부 체크(최대 레벨이 6이라면)
        foreach (WeaponBase weapon in _weapons)
        {
            // 최대 레벨이 6이며(각성 가능한 무기 체크) 현재 레벨이 5인 무기일 경우
            if (weapon.MaxLevel == 6 && weapon.Level == 5)
            {
                // 각성 설정 데이터 찾기
                SO_WeaponAwakeningData data = _awakeningDatas.Find(i => i.BaseWeapon == weapon.WeaponName);
                if (data != null)
                {
                    // 각성에 필요한 패시브가 있는지 여부 판단
                    PassiveBase requirePassive = _passives.Find(i => i.PassiveName == data.Passive);
                    if (requirePassive != null && requirePassive.Level >= 1)
                    {
                        upgradables.Add(weapon);
                        if (upgradables.Count == 3)
                            break;
                    }
                }
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