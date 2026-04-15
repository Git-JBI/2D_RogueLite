using System.Collections;
using UnityEngine;

public class EnemyDeadState : MonoBehaviour, IState
{
    private EnemySpriteController _spriteController;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _collider = GetComponentInChildren<Collider2D>();
    }

    public void Init(EnemySpriteController spriteController)
    {
        _spriteController = spriteController;
    }

    private IEnumerator DeadCoroutine()
    {
        // 리지드바디 비활성화
        _rigidbody.simulated = false;

        // 콜라이더 비활성화
        _collider.enabled = false;

        // 죽음 애니메이션 트리거실행
        _spriteController.Dead();

        yield return new WaitForSeconds(0.5f);

        Debug.Log("사망");
        //TODO 추후 ObjectPooling으로 변환 시 수정
        Destroy(gameObject);
    }

    public void OnStateEnter()
    {
        Debug.Log("Dead Enter");
        
        StartCoroutine(DeadCoroutine());
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {
    }
}
