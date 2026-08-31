using System.Collections.Generic;
using UnityEngine.Events;

public interface IUpgradeViewer
{
    public event UnityAction OnSelected;

    void Show(List<IUpgradable> upgradables);
    void Hide();
}
