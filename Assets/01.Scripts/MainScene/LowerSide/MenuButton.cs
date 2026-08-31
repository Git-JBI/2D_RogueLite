using UnityEngine.Events;

public class MenuButton : ButtonBase
{
    int _menuNumber;

    public event UnityAction<int> OnMenuButtonClicked;
    public event UnityAction OnButtonClicked;

    public void Init(int number)
    {
        _menuNumber = number;
    }

    protected override void ClickButton()
    {
        MenuButtonClick();
    }

    private void MenuButtonClick()
    {
        OnMenuButtonClicked?.Invoke(_menuNumber);
        OnButtonClicked?.Invoke();
    }
}
