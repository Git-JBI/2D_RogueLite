using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_LoadGameScene : ButtonBase
{
    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    protected override void ClickButton()
    {
        LoadGameScene();
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(2);
    }
}
