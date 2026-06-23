using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_LoadMainScene : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(LoadMainScene);
    }

    private void LoadMainScene()
    {
        // 게임 씬에서 정지된 시간 정상화
        Time.timeScale = 1.0f;

        // 메인 씬로드
        SceneManager.LoadScene("MainScene");
    }
}
