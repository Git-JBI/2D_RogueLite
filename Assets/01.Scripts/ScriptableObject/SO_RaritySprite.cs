using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/RaritySpriteData", fileName = "RaritySpriteData")]
public class SO_RaritySprite : ScriptableObject
{
    [SerializeField] private Sprite[] _squareSprites;
    [SerializeField] private Sprite[] _circleSprites;
    [SerializeField] private Sprite[] _diamondSprites;

    public Sprite[] SquareSprites => _squareSprites;
    public Sprite[] CircleSprites => _circleSprites;
    public Sprite[] DiamondSprites => _diamondSprites;
}
