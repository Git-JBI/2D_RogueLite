using System;
using UnityEngine;
using static UnityEngine.Rendering.CoreUtils;

[Serializable]
public class ProbabilitySection
{
    [SerializeField] private float _first;
    [SerializeField] private float _second;
    [SerializeField] private float _third;

    public float GetProbability(int index)
    {
        switch (index)
        {
            case 0:
                return _first;
            case 1:
                return _second;
            case 2:
                return _third;
            default:
                return 0;
        }
    }
}

[CreateAssetMenu(menuName = "GameDatas/ExpProbabilityData", fileName = "ExpTimeConfigData")]
public class SO_ExpProbabilityConfigData : ScriptableObject
{
    [SerializeField] private ProbabilitySection[] _sections;

    public ProbabilitySection[] Sections => _sections;

    private ProbabilitySection GetSection(int index)
    {
        if (index < 0 || index >= _sections.Length)
            throw new ArgumentOutOfRangeException(nameof(index));

        return _sections[index];
    }

    public float GetProbability(int sectionIndex, int resultIndex)
    {
        return GetSection(sectionIndex).GetProbability(resultIndex);
    }
}