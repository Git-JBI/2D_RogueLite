using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/HeroUIAnimData", fileName = "HeroUIAnimData")]
public class SO_HeroUIAnimData : ScriptableObject
{
    [SerializeField] private RuntimeAnimatorController[] _animControllers;

    public RuntimeAnimatorController[] AnimatorController => _animControllers;
}
