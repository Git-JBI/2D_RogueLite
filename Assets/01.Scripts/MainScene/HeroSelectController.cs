using UnityEngine;

public class HeroSelectController : MonoBehaviour
{
    [SerializeField] private HeroData[] _heroDatas;
    [SerializeField] private Button_SelectHero _heroButton;

    [SerializeField] private HeroSelectViewer _viewer;

    private void Start()
    {
        for(int i = 0; i < _heroDatas.Length; i++)
        {
            Button_SelectHero button = Instantiate(_heroButton, transform);
            button.Init(_heroDatas[i]);
            button.OnSpriteChange += _viewer.SetHeroImage;
            button.OnTextChange += _viewer.SetHereDescText;
        }

        HeroData herodata = GameManager.Instance.GameModel.HeroData;

        _viewer.Init(herodata.HeroSprite, herodata.Desc);
    }
}
