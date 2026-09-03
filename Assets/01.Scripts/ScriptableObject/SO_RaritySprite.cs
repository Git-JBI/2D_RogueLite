using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/RaritySpriteData", fileName = "RaritySpriteData")]
public class SO_RaritySprite : ScriptableObject
{
    [SerializeField] private Sprite[] _sprites;

    public Sprite[] Sprites => _sprites;
}
