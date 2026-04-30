using UnityEngine;

public abstract class PassiveBase : MonoBehaviour, IUpgradable
{
    //TODO 추후 불러오는방법 변경 SerializeField X
    [SerializeField] protected PassiveData _passiveData;

    protected HeroModel _model;

    protected int _level;

    public Sprite IconSprite => _passiveData.PassiveSprite;

    public int Level => _level;

    public string Desc => _passiveData.Desces[_level];

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
