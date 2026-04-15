using UnityEngine;

public interface IState
{
    void OnStateEnter();
    void OnStateUpdate();
    void OnStateExit();
}
