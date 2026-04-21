using UnityEngine;

public class BulletBase : MonoBehaviour
{
    protected float _damage;

    private void Awake()
    {
        _damage = 5.0f;
    }
}
