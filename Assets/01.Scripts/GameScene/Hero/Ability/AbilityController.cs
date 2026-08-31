using UnityEngine;

public class AbilityController : MonoBehaviour
{
    private AbilityGold _abilityGold;
    private AbilityHeal _abilityHeal;

    private void Awake()
    {
        _abilityGold = GetComponentInChildren<AbilityGold>();
        _abilityHeal = GetComponentInChildren<AbilityHeal>();
    }

    public void Init(GameSceneModel gameSceneModel, HeroModel heroModel)
    {
        _abilityGold.Init(gameSceneModel);
        _abilityHeal.Init(heroModel);
    }
}