using UnityEngine;

public class TrailWeaponObjectBase : PlayerWeaponObjectBase
{
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        Debug.Log("µîÀå");
        _trailRenderer.Clear();
    }

    private void OnDisable()
    {
        Debug.Log("»ç¶óÁü");
        _trailRenderer.emitting = false;
        _trailRenderer.Clear();
    }

    protected void StartTrail()
    {
        _trailRenderer.Clear();
        _trailRenderer.emitting = true;
    }
}
        //// 1. ¿ùµå ÁÂÇ¥°è -> È­¸é ÁÂÇ¥
        //Vector2 screenPoint = Camera.main.WorldToScreenPoint();

        //// 2. È­¸é ÁÂÇ¥ -> UI ÁÂÇ¥
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(null, screenPoint.magnitude, null, out Vector2 localPoint);
