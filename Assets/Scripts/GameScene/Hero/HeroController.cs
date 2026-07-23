using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D.Animation;

public class HeroController : MonoBehaviour, IDamageable, IItem
{
    private HeroModel _heromodel;
    private HeroMovement _movement;
    private HeroSpriteController _spriteController;
    private HeroHud _heroHud;
    private WeaponController _weaponController;
    private UpgradeController _upgradeController;
    private PassiveController _passiveController;

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
        _weaponController = GetComponentInChildren<WeaponController>();
        _passiveController = GetComponentInChildren<PassiveController>();
        _heroHud = GetComponentInChildren<HeroHud>();
    }

    public void Init(IUpgradeViewer upgradeViewer)
    {
        SpriteLibraryAsset libraryAsset = Resources.Load<SpriteLibraryAsset>($"SpriteLibrary/Hero/Hero{GameManager.Instance.GameModel.HeroData.HeroName}");

        _spriteController.Init(libraryAsset);
        _weaponController.Init(_heromodel);
        _passiveController.Init(_heromodel);
        _upgradeController.Init(upgradeViewer, _weaponController, _passiveController);

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
    public void TakeDamage(float damage)
    {
        _heromodel.ChangeHp(damage);

        if(_heromodel.CurHp <= 0)
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
