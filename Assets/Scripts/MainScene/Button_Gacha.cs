using UnityEngine;
using UnityEngine.Events;

public class Button_Gacha : ButtonBase
{
    private GachaManager _gachaManager;

    public void Init(GachaManager manager)
    {
        _gachaManager = manager;

        base.Init();
    }

    protected override void ClickButton()
    {
        _gachaManager.Gacha();
    }
}
