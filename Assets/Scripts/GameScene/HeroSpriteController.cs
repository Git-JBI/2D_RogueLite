using UnityEngine;
using UnityEngine.U2D.Animation;

public class HeroSpriteController : SpriteControllerBase
{
    private SpriteLibrary _spriteLibrary;

    private SpriteLibraryAsset _spriteLibraryAsset;

    protected override void Awake()
    {
        base.Awake();

        _spriteLibrary = GetComponent<SpriteLibrary>();
    }

    public void Init(SpriteLibraryAsset asset)
    {
        _spriteLibraryAsset = asset;
    }

    private void SetHeroSpriteLibraryAsset()
    {
        _spriteLibrary.spriteLibraryAsset = _spriteLibraryAsset;
    }
}
