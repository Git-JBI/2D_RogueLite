using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameDatas/StageData", fileName = "StageData")]
public class SO_StageData : ScriptableObject
{
    public int Stage;
    public float StageTimeLimit;

    public List<EnemyController> Enemies;
    
    public List<EnemyController> Boss;
}
