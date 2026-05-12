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

        _backgroundRadius = _joystick[1].rectTransform.rect.width / 2;

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

    private void SetStickPos(Vector3 inputPos)
    {
        float distance = Vector2.Distance(_joystick[1].transform.position, inputPos);
        Vector2 stickPos;

        stickPos = _backgroundRadius > distance ? inputPos : _joystick[1].transform.position + (inputPos - _joystick[1].transform.position).normalized * _backgroundRadius;


        _joystick[2].transform.position = stickPos;
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

        //TODO 마지막 터치 시 플립x방향 고정
        OnTouchUp?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 스틱 이동
        SetStickPos(eventData.position);

        // 이동 방향
        _moveDir = ((Vector2)_joystick[2].rectTransform.position - _pointerDownPosition).normalized;

        //TODO 드래그가 이루어지지않아서 밀림현상 발생
        OnDragging?.Invoke(_moveDir);
    }
}
