using UnityEngine;

public class MainSceneUIManager : MonoBehaviour
{
    private HeroSelectViewer _heroSelectViewer;
    private MenuController _menuController;

    private void Awake()
    {
        _heroSelectViewer = GetComponentInChildren<HeroSelectViewer>();
        _menuController = GetComponentInChildren<MenuController>();
    }


}
