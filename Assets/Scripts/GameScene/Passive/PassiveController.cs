using UnityEngine;

public class PassiveController : MonoBehaviour
{
    private PassiveBase[] _passives;

    private HeroModel _model;

    private void Awake()
    {
        _passives = GetComponentsInChildren<PassiveBase>();
    }

    public void Init(HeroModel model)
    {
        _model = model;

        foreach (PassiveBase x in _passives)
        {
            x.Init(_model);
        }
    }
}
