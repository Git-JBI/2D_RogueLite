using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 inputAxis, float moveSpeed)
    {
        _rigidbody.linearVelocity = inputAxis * moveSpeed;
    }
}
