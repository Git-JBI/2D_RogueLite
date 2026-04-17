using UnityEngine;

public class WeaponShovel : MonoBehaviour
{
    private float _rotSpeed;

    private void Awake()
    {
        _rotSpeed = 5.0f;
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, _rotSpeed);
    }
}
