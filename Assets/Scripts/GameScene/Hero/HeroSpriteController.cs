using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class HeroSpriteController : SpriteControllerBase
{
    private SpriteLibrary _spriteLibrary;

    private SpriteLibraryAsset _heroSpriteLibraryAsset;

    protected override void Awake()
    {
        base.Awake();

        _spriteLibrary = GetComponent<SpriteLibrary>();
    }

    //TODO 여기서 불러오도록 설정
    public void Init(SpriteLibraryAsset asset)
    {
        _heroSpriteLibraryAsset = asset;

        SetHeroSpriteLibraryAsset();
    }

    public void MoveStart()
    {
        _animator.SetBool("IsMove", true);
    }

    public void MoveFinish()
    {
        _animator.SetBool("IsMove", false);
    }

    private void SetHeroSpriteLibraryAsset()
    {
        _spriteLibrary.spriteLibraryAsset = _heroSpriteLibraryAsset;
    }
}
