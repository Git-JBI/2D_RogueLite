using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HeroButton : ButtonBase
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

    //TODO 캐릭터마다 다른 기본무기, 캐릭터 고유 능력치 적용 필요
    protected override void ClickButton()
    {
        GameManager.Instance.GameData.HeroData = _heroData;
        OnSpriteChange?.Invoke(_heroData.HeroSprite);
        OnTextChange?.Invoke(_heroData.Desc);
    }
}
