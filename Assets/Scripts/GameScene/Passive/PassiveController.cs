using UnityEngine;

public class PassiveController : MonoBehaviour
{
    private PassiveBase[] _passives;

    private HeroModel _model;

    public void Init(HeroModel model)
    {
        _passives = GetComponentsInChildren<PassiveBase>();

        _model = model;

        foreach (PassiveBase x in _passives)
        {
            x.Init(_model);
        }
    }
}
