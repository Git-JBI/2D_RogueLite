using UnityEngine;

public class AbilityHeal : MonoBehaviour, IUpgradable
{
    public Sprite IconSprite => throw new System.NotImplementedException();

    public int Level => 99;

    public string Desc => "체력을 25% 회복";

    public void Upgrade()
    {

    }
}