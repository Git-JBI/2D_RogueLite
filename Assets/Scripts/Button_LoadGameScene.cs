using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_LoadGameScene : ButtonBase
{
    protected override void Awake()
    {
        base.Awake();

        _button.onClick.AddListener(LoadGameScene);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(2);
    }
}
