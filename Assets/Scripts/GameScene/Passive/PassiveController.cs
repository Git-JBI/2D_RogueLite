using UnityEngine;

public class PassiveController : MonoBehaviour
{
    private PassiveBase[] _passives;

    public PassiveBase[] Passives => _passives;

    private void Awake()
    {
        _passives = GetComponentsInChildren<PassiveBase>();
    }

    public void Init(HeroModel model)
    {
        foreach (PassiveBase x in _passives)
        {
            x.Init(model);
        }
    }
}
