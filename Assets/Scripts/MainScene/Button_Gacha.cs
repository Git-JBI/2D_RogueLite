using UnityEngine;
using UnityEngine.Events;

public class Button_Gacha : ButtonBase
{
    public event UnityAction OnButtonClicked;

    protected override void ClickButton()
    {
        ButtonClicked();
    }

    private void ButtonClicked()
    {
        OnButtonClicked?.Invoke();
    }
}
