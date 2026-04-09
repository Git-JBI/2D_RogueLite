using UnityEngine;
using UnityEngine.UI;

public class ButtonBase : MonoBehaviour
{
    protected Button _button;

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();
    }
}
