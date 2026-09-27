using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D.Animation;

public class HeroController : MonoBehaviour, IDamageable, IItem
{
    private HeroModel _heromodel;
    private HeroMovement _movement;
    private HeroSpriteController _spriteController;
    private HeroHud _heroHud;
    private UpgradeController _upgradeController;
    private HeroWeaponController _weaponController;
    private PassiveController _passiveController;
    private AbilityController _abilityController;

    public event UnityAction<int> OnChangeLevel
    {
        add { _heromodel.OnChangeLevel += value; }
        remove { _heromodel.OnChangeLevel -= value;}
    }
    public event UnityAction<float, float> OnChangeExp
    {
        add { _heromodel.OnChangeExp += value; }
        remove { _heromodel.OnChangeExp -= value; }
    }
    public event UnityAction OnDead;

    private void Awake()
    {
        _movement = GetComponent<HeroMovement>();
        _heromodel = GetComponent<HeroModel>();
        _upgradeController = GetComponent<UpgradeController>();

        _spriteController = GetComponentInChildren<HeroSpriteController>();
        _heroHud = GetComponentInChildren<HeroHud>();
        _weaponController = GetComponentInChildren<HeroWeaponController>();
        _passiveController = GetComponentInChildren<PassiveController>();
        _abilityController = GetComponentInChildren<AbilityController>();
    }

    public void Init(GameSceneModel model ,IUpgradeViewer upgradeViewer)
    {
        SpriteLibraryAsset libraryAsset = Resources.Load<SpriteLibraryAsset>($"SpriteLibrary/Hero/Hero_{GameManager.Instance.GameModel.HeroData.HeroName}");

        _spriteController.Init(libraryAsset);
        _weaponController.Init(_heromodel);
        _passiveController.Init(_heromodel);
        _upgradeController.Init(upgradeViewer, _weaponController, _passiveController);
        _abilityController.Init(model, _heromodel);

        _heromodel.OnChangeLevel += _upgradeController.OnLevelUp;
        _heromodel.OnChangeHp += _heroHud.SetHpBar;
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
    public void TakeDamage(float damage, bool isCritical)
    {
        _heromodel.TakeDamage(damage);

        if(!_heromodel.IsAlive)
        {
            Time.timeScale = 0;
            OnDead?.Invoke();
        }
    }

    public void GetExpItem(int amount)
    {
        _heromodel.IncreaseExp(amount);
    }
    #endregion
}
