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
        // 게임 씬에서 정지된 시간 정상화
        Time.timeScale = 1.0f;

        // 메인 씬로드
        SceneManager.LoadScene(1);
    }
}
