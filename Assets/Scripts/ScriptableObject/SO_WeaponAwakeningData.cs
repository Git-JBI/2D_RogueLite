using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/WeaponAwakeningData", fileName = "WeaponAwakeningData")]
public class SO_WeaponAwakeningData : ScriptableObject
{
    [SerializeField] private WeaponName _baseWeapon;
    [SerializeField] private PassiveName _basePassive;
    [SerializeField] private AwakeningWeaponName _resultWeapon;

    public WeaponName BaseWeapon => _baseWeapon;
    public PassiveName Passive => _basePassive;
    public AwakeningWeaponName ResultWeapon => _resultWeapon;
}
