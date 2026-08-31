using DG.Tweening;
using UnityEngine;

public enum MenuNumber
{
    Shop,
    Character,
    Stage,
    Upgrade,
}

public class MenuPresenter
{
    private MenuViewer _viewer;
    private MenuNumber _menuNumber;

    public MenuPresenter(MenuViewer viewer)
    {
        _viewer = viewer;
        _menuNumber = MenuNumber.Shop;
    }

    public void MoveMenuPos(int number)
    {
        // 현재 열려 있는 메뉴일 경우 Return
        if (_menuNumber == (MenuNumber)number)
            return;

        // 메뉴 위치가 움직일 정도
        Vector3 pos = new Vector3(-1080, 0, 0);

        // 메뉴 위치 변경
        _viewer.MenuTransfrom.DOMove(pos * number, 0.5f);

        // 메뉴 넘버 설정
        _menuNumber = (MenuNumber)number;

        switch(number)
        {
            case 0:
                break;
            case 1:
                _viewer.InventoryManager.Init();
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
}