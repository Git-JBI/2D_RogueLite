using UnityEngine;
using UnityEngine.UI;

public class Button_SelectHero : ButtonPressScale
{
    [SerializeField] private RuntimeAnimatorController[] _controller;
    private Button _heroButton;
    private Animator _animator;

    private SO_HeroConfigData _heroData;

    private void Awake()
    {
        _heroButton = GetComponent<Button>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _animator.runtimeAnimatorController = _controller[0];
        if (Input.GetKeyDown(KeyCode.Alpha2))
            _animator.runtimeAnimatorController = _controller[1];
        if (Input.GetKeyDown(KeyCode.Alpha3))
            _animator.runtimeAnimatorController = _controller[2];
    }

    public void Init(SO_HeroConfigData heroData)
    {
        _heroData = heroData;
    }
}
