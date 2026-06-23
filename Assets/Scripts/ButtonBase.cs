using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonBase : MonoBehaviour
{
    protected Button _button;

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void Init()
    {
        _button.onClick.AddListener(ClickButton);
    }

    protected abstract void ClickButton();
}
