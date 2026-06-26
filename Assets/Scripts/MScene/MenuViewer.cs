using UnityEngine;
using UnityEngine.UI;

public enum MenuNumber
{
    Shop,
    Character,
    Stage,
    Upgrade,
}

public class MenuViewer : MonoBehaviour
{
    private Button[] _menuButton;

    private void Awake()
    {
        _menuButton = GetComponentsInChildren<Button>();

        foreach(Button x in _menuButton)
        {
            
        }
    }
}
