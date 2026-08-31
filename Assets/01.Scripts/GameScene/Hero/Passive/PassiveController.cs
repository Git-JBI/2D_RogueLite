using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PassiveController : MonoBehaviour
{
    private PassiveBase[] _passives;
    private IUpgradable[] _upgradables;

    public IReadOnlyList<IUpgradable> Upgradables => _upgradables;

    private void Awake()
    {
        _passives = GetComponentsInChildren<PassiveBase>();
        _upgradables = GetComponentsInChildren<IUpgradable>();
    }

    public void Init(HeroModel model)
    {
        foreach (PassiveBase x in _passives)
        {
            x.Init(model);
        }
    }

    public IUpgradable GetIUpgradablePassive(PassiveType type)
    {
        return _passives.First(x => x.PassiveType == type);
    }
}
