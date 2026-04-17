using UnityEngine;
using UnityEngine.U2D.Animation;

public class HeroController : MonoBehaviour, IDamageable
{
    private RigidMovement _movement;
    private HeroModel _heromodel;
    private HeroSpriteController _spriteController;
    private HeroHud _heroHud;

    private void Awake()
    {
        _movement = GetComponent<RigidMovement>();
        _heromodel = GetComponent<HeroModel>();

        _spriteController = GetComponentInChildren<HeroSpriteController>();
        _heroHud = GetComponentInChildren<HeroHud>();

        _heromodel.OnChangeHp += _heroHud.SetHpBar;
    }

    public void Init(SpriteLibraryAsset asset)
    {
        _spriteController.Init(asset);
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
    #endregion
}
