using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/UpgradeData", fileName = "UpgradeData")]
public class UpgradeData : ScriptableObject
{
    [SerializeField] private int[] _value;
    [SerializeField] private int[] _price;

    public int[] Value => _value;
    public int[] Price => _price;
}
