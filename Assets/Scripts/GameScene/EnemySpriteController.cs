using UnityEngine;

public class EnemySpriteController : SpriteControllerBase
{
    public void Damaged()
    {
        _animator.SetTrigger("Damaged");
    }
}
