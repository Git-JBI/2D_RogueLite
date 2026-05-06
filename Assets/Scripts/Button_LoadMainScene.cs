using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_LoadMainScene : ButtonBase
{
    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    protected override void ClickButton()
    {
        LoadMainScene();
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }
}
