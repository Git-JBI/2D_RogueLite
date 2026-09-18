using UnityEngine;

public class TrailBulletBase : PlayerWeaponObjectBase
{
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        _trailRenderer.Clear();
        _trailRenderer.emitting = true;
    }

    private void OnDisable()
    {
        _trailRenderer.emitting = false;
        _trailRenderer.Clear();
    }
}
