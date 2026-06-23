using UnityEngine;

public class SpriteControllerBase : MonoBehaviour
{
    protected SpriteRenderer _spriteRenderer;
    protected Animator _animator;

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public void SetFlipX(float dirX)
    {
        if(dirX >= 0)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }
}
