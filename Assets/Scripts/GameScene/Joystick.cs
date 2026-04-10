using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    // [1]: Background, [2]: Stick
    private Image[] _joystick;

    private float _backgroundRadius;
    private Vector2 _downPosition;

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
        _downPosition = eventData.position;

        JoystickSetActiveTrue();

        _joystick[1].rectTransform.position = _downPosition;
        _joystick[2].rectTransform.position = _downPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        JoystickSetActiveFalse();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _joystick[2].rectTransform.position = eventData.position;
    }
}
