using UnityEngine;
using UnityEngine.U2D.Animation;

public class HeroController : MonoBehaviour, IDamageable, Iitemable
{
    private RigidMovement _movement;
    private HeroModel _heromodel;
    private HeroSpriteController _spriteController;
    private HeroHud _heroHud;
    private UpgradeController _upgradeController;

    private UIManager _uiManager;

    private void Awake()
    {
        _movement = GetComponent<RigidMovement>();
        _heromodel = GetComponent<HeroModel>();
        _upgradeController = GetComponent<UpgradeController>();

        _spriteController = GetComponentInChildren<HeroSpriteController>();
        _heroHud = GetComponentInChildren<HeroHud>();

        _heromodel.OnChangeHp += _heroHud.SetHpBar;
        _heromodel.OnChangeLevel += _upgradeController.OnLevelUp;
    }
    
    public void Init(SpriteLibraryAsset asset, UIManager ui)
    {
        _spriteController.Init(asset);
        _uiManager = ui;

        _heromodel.OnChangeLevel += _uiManager.SetLevelText;
        _heromodel.OnChangeLevel += _upgradeController.OnLevelUp;
        _heromodel.OnChangeExp += _uiManager.SetExpBar;
        _upgradeController.SetUpgradeActive += _uiManager.SetUpgradePanelActive;

        _uiManager.SetLevelText(_heromodel.Level);
        _uiManager.SetExpBar(_heromodel.CurExp, _heromodel.MaxExp);
    }

    public void MoveStart()
    {
        _spriteController.MoveStart();
    }

    public void MoveFinish()
    {
        _spriteController.MoveFinish();
    }

    public void Move(Vector2 axis)
    {
        _movement.Move(axis, _heromodel.MoveSpeed);
        _spriteController.SetFlipX(axis.x);
    }

    #region Interface
    public void TakeDamage(float damage)
    {
        _heromodel.TakeDamage(damage);
    }

    public void GetExpItem(int amount)
    {
        _heromodel.IncreaseExp(amount);
    }
    #endregion
}
