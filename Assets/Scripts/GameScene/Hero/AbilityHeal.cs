using UnityEngine;

public class AbilityHeal : MonoBehaviour, IUpgradable
{
    [SerializeField] private Sprite _healSprite;

    public Sprite IconSprite => _healSprite;

    public int Level => 0;

    public string Desc => "체력을 25% 회복";

    public bool IsPermanentAbility => false;

    public void Upgrade()
    {

    }
}