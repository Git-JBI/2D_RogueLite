using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectViewer : MonoBehaviour
{
    [SerializeField] private Image _heroImage;
    [SerializeField] private TextMeshProUGUI _heroDesc;

    public void Init(Sprite sprite, string desc)
    {
        SetHeroImage(sprite);
        SetHereDescText(desc);
    }

    public void SetHeroImage(Sprite heroSprite)
    {
        _heroImage.sprite = heroSprite;
    }

    public void SetHereDescText(string desc)
    {
        _heroDesc.text = desc;
    }
}
