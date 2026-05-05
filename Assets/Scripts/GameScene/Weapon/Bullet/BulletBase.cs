using UnityEngine;

public class BulletBase : MonoBehaviour
{
    protected HeroModel _model;
    protected float _damageMagnification;

    public void Init(HeroModel model, float damageMagnification)
    {
        _model = model;
        _damageMagnification = damageMagnification;
    }
}