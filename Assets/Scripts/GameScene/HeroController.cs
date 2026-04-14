using UnityEngine;
using UnityEngine.U2D.Animation;

public class HeroController : MonoBehaviour
{
    private RigidMovement _movement;
    private HeroSpriteController _spriteController;

    private void Awake()
    {
        _movement = GetComponent<RigidMovement>();

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
}
