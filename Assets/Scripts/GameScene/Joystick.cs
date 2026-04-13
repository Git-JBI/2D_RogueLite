using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    // [1]: Background, [2]: Stick
    private Image[] _joystick;

    private Vector2 _pointerDownPosition;
    private Vector2 _moveDir;

    private float _backgroundRadius;

    public event Action OnTouchDown;
    public event Action OnTouchUp;
    public event Action<Vector2> OnDragging;

    private void Awake()
    {
        _joystick = GetComponentsInChildren<Image>();

        JoystickSetActiveFalse();
    }

    private void JoystickSetActiveTrue()
    {
        _joystick[1].gameObject.SetActive(true);
        _joystick[2].gameObject.SetActive(true);
    }

    private void JoystickSetActiveFalse()
    {
        _joystick[1].gameObject.SetActive(false);
        _joystick[2].gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 움직임 시작 
        _pointerDownPosition = eventData.position;

        JoystickSetActiveTrue();

        _joystick[1].rectTransform.position = _pointerDownPosition;
        _joystick[2].rectTransform.position = _pointerDownPosition;

        OnTouchDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 움직임 끝
        JoystickSetActiveFalse();
        OnDragging?.Invoke(Vector2.zero);

        OnTouchUp?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 스틱 이동
        _joystick[2].rectTransform.position = eventData.position;

        // 이동 방향
        _moveDir = ((Vector2)_joystick[2].rectTransform.position - _pointerDownPosition).normalized;

        OnDragging?.Invoke(_moveDir);
    }
}
