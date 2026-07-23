using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/WeaponAwakeningData", fileName = "WeaponAwakeningData")]
public class SO_AwakeningConfigData : ScriptableObject
{
    [SerializeField] private WeaponType _baseWeapon;
    [SerializeField] private PassiveType _basePassive;
    [SerializeField] private AwakeningWeaponType _resultWeapon;

    public WeaponType BaseWeapon => _baseWeapon;
    public PassiveType Passive => _basePassive;
    public AwakeningWeaponType ResultWeapon => _resultWeapon;
}
