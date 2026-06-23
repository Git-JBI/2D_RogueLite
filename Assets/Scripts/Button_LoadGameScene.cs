using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_LoadGameScene : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(LoadGameScene);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
