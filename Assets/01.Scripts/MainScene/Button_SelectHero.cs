using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Button_SelectHero : ButtonPressScale
{
    private Image _buttonImage;

    private HeroData _heroData;

    public event UnityAction<Sprite> OnSpriteChange;
    public event UnityAction<string> OnTextChange;

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
    }

    public void Init(HeroData heroData)
    {
        _heroData = heroData;

        _buttonImage.sprite = _heroData.HeroSprite;
    }

    private void ClickButton()
    {
        GameManager.Instance.GameModel.SelectHero(_heroData);
        OnSpriteChange?.Invoke(_heroData.HeroSprite);
        OnTextChange?.Invoke(_heroData.Desc);
    }
}
