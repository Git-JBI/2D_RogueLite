using UnityEngine;
using UnityEngine.U2D.Animation;

public class HeroController : MonoBehaviour
{
    private HeroSpriteController _spriteController;

    private SpriteLibraryAsset _spriteLibraryAsset;

    private void Awake()
    {
        _spriteController = GetComponent<HeroSpriteController>();
    }

    public void Init(SpriteLibraryAsset asset)
    {
        _spriteLibraryAsset = asset;

        _spriteController.Init(_spriteLibraryAsset);
    }
}
