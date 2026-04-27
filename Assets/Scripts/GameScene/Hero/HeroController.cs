using UnityEngine;
using UnityEngine.U2D.Animation;

public class HeroController : MonoBehaviour, IDamageable, Iitemable
{
    private RigidMovement _movement;
    private HeroModel _heromodel;
    private HeroSpriteController _spriteController;
    private HeroHud _heroHud;
    private UIManager _uiManager;

    private void Awake()
    {
        _movement = GetComponent<RigidMovement>();
        _heromodel = GetComponent<HeroModel>();

        _spriteController = GetComponentInChildren<HeroSpriteController>();
        _heroHud = GetComponentInChildren<HeroHud>();

        _heromodel.OnChangeHp += _heroHud.SetHpBar;
    }

    public void Init(SpriteLibraryAsset asset, UIManager ui)
    {
        _spriteController.Init(asset);
        _uiManager = ui;
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
        Debug.Log(_heromodel.CurExp);
        //TODO 이벤트로 바꿀까 생각
        _uiManager.SetExpBar(_heromodel.CurExp / (float)_heromodel.MaxExp);
        _uiManager.SetLevelText(_heromodel.Level);
    }
    #endregion
}
