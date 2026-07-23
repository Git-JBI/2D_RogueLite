using UnityEngine;

public enum PassiveType
{
    Damage,
    Hp,
    MoveSpeed,
}

public abstract class PassiveBase : MonoBehaviour, IUpgradable
{
    protected SO_PassiveData _passiveData;
    protected HeroModel _model;

    protected int _level;

    public PassiveType PassiveName => _passiveData.PassiveName;
    public Sprite IconSprite => _passiveData.PassiveSprite;
    public int Level => _level;
    public int MaxLevel => _passiveData.Desces.Length;
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
