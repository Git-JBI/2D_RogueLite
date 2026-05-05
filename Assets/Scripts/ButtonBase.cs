using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonBase : MonoBehaviour
{
    protected Button _button;

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();
    }
}
