using UnityEngine;

public class GameSceneModel : MonoBehaviour
{
    private float _stageTime;

    public float StageTime => _stageTime;

    public void IncreaseStageTime(float time)
    {
        _stageTime += time;
    }
}
