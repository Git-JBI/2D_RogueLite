public class EnemySpriteController : SpriteControllerBase
{
    public void Damaged()
    {
        _animator.SetTrigger("Damaged");
    }

    public void Dead()
    {
        _animator.SetTrigger("Dead");
    }
}
