using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Button_SelectHero : ButtonBase
{
    private Image _buttonImage;

    private HeroData _heroData;

    public event UnityAction<Sprite> OnSpriteChange;
    public event UnityAction<string> OnTextChange;

    protected override void Awake()
    {
        base.Awake();

        _buttonImage = GetComponent<Image>();
    }

    public void Init(HeroData heroData)
    {
        _heroData = heroData;

        _buttonImage.sprite = _heroData.HeroSprite;

        base.Init();
    }

    protected override void ClickButton()
    {
        GameManager.Instance.GameModel.SelectHero(_heroData);
        OnSpriteChange?.Invoke(_heroData.HeroSprite);
        OnTextChange?.Invoke(_heroData.Desc);
    }
}
