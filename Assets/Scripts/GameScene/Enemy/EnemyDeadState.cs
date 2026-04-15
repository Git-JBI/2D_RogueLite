using UnityEngine;

public class EnemyDeadState : MonoBehaviour, IState
{
    public void OnStateEnter()
    {
        Debug.Log("Dead Enter");
    }

    public void OnStateExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnStateUpdate()
    {
        throw new System.NotImplementedException();
    }
}
