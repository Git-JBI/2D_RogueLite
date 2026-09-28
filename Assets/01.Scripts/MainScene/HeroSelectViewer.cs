using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class HeroSelectViewer : MonoBehaviour
{
    [SerializeField] private Animator _heroAnimator;
    [SerializeField] private TextMeshProUGUI _heroDesc;

    public void Init(AnimatorController animController, string desc)
    {
        SetHeroAnimController(animController);
        SetHereDescText(desc);
    }

    public void SetHeroAnimController(AnimatorController animController)
    {
        _heroAnimator.runtimeAnimatorController = animController;
    }

    public void SetHereDescText(string desc)
    {
        _heroDesc.text = desc;
    }
}
