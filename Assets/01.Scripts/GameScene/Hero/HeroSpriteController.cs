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
        HeroModel heroModel = GameManager.Instance.GameModel.HeroModel;

        if (!heroModel.TryGetHeroConfigData(heroModel.GetHeroId(), out SO_HeroConfigData data))
            return;

        SpriteLibraryAsset libraryAsset = Resources.Load<SpriteLibraryAsset>($"SpriteLibrary/Hero/Hero_{data.HeroName.ToString()}");

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
