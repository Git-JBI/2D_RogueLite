using System.Collections.Generic;
using UnityEngine;

public class HeroSelectController : MonoBehaviour
{
    [SerializeField] private HeroData[] _heroDatas;
    [SerializeField] private HeroButton _heroButton;

    private void Start()
    {
        for(int i = 0; i < _heroDatas.Length; i++)
        {
            HeroButton button = Instantiate(_heroButton, transform);
            button.Init(_heroDatas[i].HeroSprite, _heroDatas[i].HeroName);
        }
    }
}
