using UnityEngine;

public class RigidMovement : MonoBehaviour
{
    private Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 inputAxis, float moveSpeed)
    {
        _rigid.linearVelocity = inputAxis * moveSpeed;
    }
}
