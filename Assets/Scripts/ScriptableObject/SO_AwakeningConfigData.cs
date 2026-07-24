using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/WeaponAwakeningData", fileName = "WeaponAwakeningData")]
public class SO_AwakeningConfigData : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private WeaponType _baseWeapon;
    [SerializeField] private PassiveType _basePassive;
    [SerializeField] private AwakeningWeaponType _resultWeapon;
    [SerializeField] private string _desc;

    public Sprite Sprite => _sprite;
    public WeaponType BaseWeapon => _baseWeapon;
    public PassiveType Passive => _basePassive;
    public AwakeningWeaponType ResultWeapon => _resultWeapon;
    public string Desc => _desc;
}