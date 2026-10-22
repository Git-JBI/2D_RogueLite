using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/MainScene/PurchaseConfigData", fileName = "PurchaseConfigData")]
public class SO_CurrencyPurchaseConfigData : ScriptableObject
{
    [SerializeField] private int[] _requires;
    [SerializeField] private int[] _rewards;

    public int[] Requires => _requires;
    public int[] Rewards => _rewards;
}
