using UnityEngine;

public class HeroSelectController : MonoBehaviour
{
    [SerializeField] private HeroData[] _heroDatas;
    [SerializeField] private HeroButton _heroButton;

    [SerializeField] private HeroSelectViewer _viewer;

    private void Start()
    {
        for(int i = 0; i < _heroDatas.Length; i++)
        {
            HeroButton button = Instantiate(_heroButton, transform);
            button.Init(_heroDatas[i]);
            button.OnSpriteChange += _viewer.SetHeroImage;
            button.OnTextChange += _viewer.SetHereDescText;
        }

        HeroData herodata = GameManager.Instance.GameModel.HeroData;

        _viewer.Init(herodata.HeroSprite, herodata.Desc);

        if (GameManager.Instance.GameModel.HeroData == null)
            _viewer.Init(_heroDatas[0].HeroSprite, _heroDatas[0].Desc);
        else
            _viewer.Init(GameManager.Instance.GameModel.HeroData.HeroSprite, GameManager.Instance.GameModel.HeroData.Desc);
    }
}
