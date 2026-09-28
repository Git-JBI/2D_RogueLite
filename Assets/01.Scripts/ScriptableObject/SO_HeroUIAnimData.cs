using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/HeroUIAnimData", fileName = "HeroUIAnimData")]
public class SO_HeroUIAnimData : ScriptableObject
{
    [SerializeField] private AnimatorController[] _animControllers;

    public AnimatorController[] AnimatorController => _animControllers;
}
