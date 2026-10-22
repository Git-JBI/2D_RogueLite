using UnityEngine.Events;

public class MenuButton : ButtonPressScale
{
    int _menuNumber;

    public event UnityAction<int> OnMenuButtonClicked;
    public event UnityAction OnButtonClicked;

    public void Init(int number)
    {
        _menuNumber = number;
    }

    private void ClickButton()
    {
        MenuButtonClick();
    }

    private void MenuButtonClick()
    {
        OnMenuButtonClicked?.Invoke(_menuNumber);
        OnButtonClicked?.Invoke();
    }
}
