using UnityEngine;

public class AbilityGold : MonoBehaviour, IUpgradable
{
    [SerializeField] private Sprite _goldSprite;

    public Sprite IconSprite => _goldSprite;

    public int Level => 0;

    public string Desc => "°ñµå¸¦ 50 È¹µæ";

    public bool IsPermanentAbility => false;

    public void Upgrade()
    {
        
    }
}
