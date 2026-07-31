using UnityEngine;

public class AbilityHeal : MonoBehaviour, IUpgradable
{
    [SerializeField] private Sprite _healSprite;
    private HeroModel _model;

    public Sprite IconSprite => _healSprite;
    public int Level => 0;
    public int MaxLevel => 0;
    public string Desc => "체력을 25% 회복";
    public bool IsPermanentAbility => false;

    public void Init(HeroModel model)
    {
        _model = model;
    }

    #region Interface
    public void Upgrade()
    {
        float heal = _model.MaxHp / 4f;

        _model.HealHp(heal);
    }
    #endregion
}