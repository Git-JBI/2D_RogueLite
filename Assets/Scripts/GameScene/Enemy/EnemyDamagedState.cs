using System.Collections;
using UnityEngine;

public class EnemyDamagedState : MonoBehaviour, IState
{
    private EnemyController _controller;
    private EnemySpriteController _spriteController;

    private Coroutine _damagedCoroutine;

    public void Init(EnemyController controller, EnemySpriteController spriteController)
    {
        _controller = controller;
        _spriteController = spriteController;
    }

    private IEnumerator DamagedCoroutine()
    {
        _spriteController.Damaged();

        // 경직 시간
        yield return new WaitForSeconds(0.2f);
    }

    public void OnStateEnter()
    {
        Debug.Log("Damaged Enter");
        _damagedCoroutine = StartCoroutine(DamagedCoroutine());
    }

    public void OnStateExit()
    {
        Debug.Log("Damaged Exit");
    }

    public void OnStateUpdate()
    {
    }
}
