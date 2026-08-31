using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/HeroConfigData/ExpConfigData", fileName = "ExpConfigData")]
public class SO_ExpConfigData : ScriptableObject
{
    // 경험치의 최대 배율은 레벨 50까지 50이상으로는 50수치 그대로
    [SerializeField] private float[] _maxExp;

    public float GetMaxExp(int level)
    {
        if (level >= 50)
            return _maxExp[49];
        else
            return _maxExp[level - 1];
    }
}