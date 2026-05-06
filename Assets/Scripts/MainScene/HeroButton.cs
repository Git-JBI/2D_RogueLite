using UnityEngine;
using UnityEngine.UI;

public class HeroButton : ButtonBase
{
    private Image _buttonImage;

    private Sprite _heroSprite;
    private string _heroName;

    protected override void Awake()
    {
        base.Awake();

        _buttonImage = GetComponent<Image>();
    }

    public void Init(Sprite sprite, string name)
    {
        _heroSprite = sprite;
        _heroName = name;

        _buttonImage.sprite = _heroSprite;

        base.Init();
    }

    protected override void ClickButton()
    {
        GameManager.Instance.GameData.SelectHeroName = _heroName;
    }
}
