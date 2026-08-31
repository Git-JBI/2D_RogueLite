using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/RarityImageColorData", fileName = "RarityImageColorData")]
public class SO_RarityImageColorData : ScriptableObject
{
    [SerializeField] private Color32[] _colors;

    public Color32[] Colors => _colors;
}
