using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_LoadMainScene : ButtonBase
{
    protected override void Awake()
    {
        base.Awake();

        _button.onClick.AddListener(LoadMainScene);
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }
}
