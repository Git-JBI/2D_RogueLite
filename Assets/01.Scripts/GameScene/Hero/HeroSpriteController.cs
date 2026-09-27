using UnityEngine;
using UnityEngine.U2D.Animation;

public class HeroSpriteController : SpriteControllerBase
{
    private SpriteLibrary _spriteLibrary;

    protected override void Awake()
    {
        base.Awake();

        _spriteLibrary = GetComponent<SpriteLibrary>();
    }

    public void Init()
    {
        SpriteLibraryAsset libraryAsset = Resources.Load<SpriteLibraryAsset>($"SpriteLibrary/Hero/Hero_{GameManager.Instance.GameModel.HeroData.HeroName}");

        SetHeroSpriteLibraryAsset(libraryAsset);
    }

    public void MoveStart()
    {
        if (_animator.GetBool("IsMove") == false)
            _animator.SetBool("IsMove", true);
    }

    public void MoveFinish()
    {
        _animator.SetBool("IsMove", false);
    }

    private void SetHeroSpriteLibraryAsset(SpriteLibraryAsset asset)
    {
        _spriteLibrary.spriteLibraryAsset = asset;
    }
}
