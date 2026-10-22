using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/ItemTypeSpriteData", fileName = "ItemTypeSpriteData")]
public class SO_ItemTypeSpriteData : ScriptableObject
{
    [SerializeField] private Sprite[] _itemTypeSprites;

    public Sprite[] ItemTypeSprites => _itemTypeSprites;
}
