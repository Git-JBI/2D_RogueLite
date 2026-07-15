using UnityEngine;

public class RigidMovement
{
    private Rigidbody2D _rigid;
    public RigidMovement(Rigidbody2D rigid)
    {
        _rigid = rigid;
    }

    public void Move(Vector2 inputAxis, float moveSpeed)
    {
        _rigid.linearVelocity = inputAxis * moveSpeed;
    }
}
