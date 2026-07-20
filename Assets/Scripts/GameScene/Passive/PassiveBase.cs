using UnityEngine;

public abstract class PassiveBase : MonoBehaviour, IUpgradable
{
    protected PassiveData _passiveData;
    protected HeroModel _model;

    protected int _level;

    public Sprite IconSprite => _passiveData.PassiveSprite;

    public int Level => _level;

    public string Desc => _passiveData.Desces[_level];

    public bool IsPermanentAbility => true;

    public void Init(HeroModel model)
    {
        _model = model;
    }

    protected abstract void CalculateStat();

    public virtual void Upgrade()
    {
        if (_level >= 5)
            return;

        _level++;

        CalculateStat();
    }
}
