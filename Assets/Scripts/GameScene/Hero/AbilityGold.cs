using UnityEngine;

public class AbilityGold : MonoBehaviour, IUpgradable
{
    [SerializeField] private Sprite _goldSprite;

    public Sprite IconSprite => _goldSprite;
    public int Level => 0;
    public int MaxLevel => 0;
    public string Desc => "골드를 50 획득";
    public bool IsPermanentAbility => false;

    public void Upgrade()
    {
        
    }
}
