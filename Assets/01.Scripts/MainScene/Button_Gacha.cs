using UnityEngine.Events;

public class Button_Gacha : ButtonBase
{
    public event UnityAction OnButtonClicked;

    private void ClickButton()
    {
        ButtonClicked();
    }

    private void ButtonClicked()
    {
        OnButtonClicked?.Invoke();
    }
}
