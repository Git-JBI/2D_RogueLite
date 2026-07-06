

public class MenuModel
{
    private MenuNumber _menuNumber;
    public MenuNumber MenuNumber => _menuNumber;

    public MenuModel()
    {
        _menuNumber = MenuNumber.Shop;
    }

    public void SetMenuNumber(int number)
    {
        _menuNumber = (MenuNumber)number;
    }
}
