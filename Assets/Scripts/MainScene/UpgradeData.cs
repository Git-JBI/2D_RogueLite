using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/UpgradeData", fileName = "UpgradeData")]
public class UpgradeData : ScriptableObject
{
    [SerializeField] private int[] _value;
}
