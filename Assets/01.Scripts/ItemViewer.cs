using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewer : MonoBehaviour
{
    [SerializeField] private Image _gradeImage;
    [SerializeField] private TextMeshProUGUI _descText;

    public virtual void Init(EquipmentItemData data, string desc)
    {
        Sprite sprite = (GameManager.Instance.ResourceManager.GetScriptable("GameDatas/RaritySpriteData/RaritySpriteData") as SO_RaritySprite).Sprites[(int)data.Rarity];
        SetRaritySprite(sprite);
        SetDescText(desc);
    }

    private void SetRaritySprite(Sprite sprite)
    {
        _gradeImage.sprite = sprite;
    }

    private void SetDescText(string desc)
    {
        _descText.text = desc;
    }
}
