using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "GameDatas/StageData", fileName = "StageData")]
public class SO_StageData : ScriptableObject
{
    public int Stage;
    public float StageTimeLimit;

    public List<EnemyController> Enemies = new();
    
    public List<EnemyController> Bosses = new();
}
