using UnityEngine;

public class SpriteControllerBase : MonoBehaviour
{
    protected SpriteRenderer _spriteRenderer;

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetFlipX(float dirX)
    {
        if(dirX > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }
}
