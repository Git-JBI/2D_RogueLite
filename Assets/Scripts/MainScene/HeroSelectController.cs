using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        _viewer.Init(_heroDatas[0].HeroSprite, _heroDatas[0].Desc);
    }
}
