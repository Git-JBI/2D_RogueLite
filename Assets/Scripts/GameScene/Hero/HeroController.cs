using UnityEngine;
using UnityEngine.U2D.Animation;

public class HeroController : MonoBehaviour, IDamageable
{
    private RigidMovement _movement;
    private HeroModel _heromodel;
    private HeroSpriteController _spriteController;

    private void Awake()
    {
        _movement = GetComponent<RigidMovement>();
        _heromodel = GetComponent<HeroModel>();

        _spriteController = GetComponentInChildren<HeroSpriteController>();
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
        //TODO 속도 고정값X
        _movement.Move(axis, 2.0f);
        _spriteController.SetFlipX(axis.x);
    }

    #region Interface
    public void TakeDamage(float damage)
    {
        _heromodel.TakeDamage(damage);
    }
    #endregion
}
