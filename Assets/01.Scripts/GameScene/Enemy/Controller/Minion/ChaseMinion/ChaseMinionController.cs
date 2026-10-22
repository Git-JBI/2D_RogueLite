public class ChaseMinionController : MinionController
{
    protected override void Awake()
    {
        base.Awake();

        _normalState = new ChaseMinionNormalState(this);
    }
}