using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    /// <summary>
    /// 메인 씬을 로드하는 함수
    /// </summary>
    public void LoadMainScene()
    {
        // 풀로 되돌리기
        GameManager.Instance.PoolManager.AllReturnPool();

        // MainScene 불러오기
        SceneManager.LoadScene("MainScene");
    }
    /// <summary>
    /// 게임 씬을 로드하는 함수
    /// </summary>
    public void LoadGameScene()
    {
        // 풀로 되돌리기
        GameManager.Instance.PoolManager.AllReturnPool();

        // GameScene 불러오기
        SceneManager.LoadScene("GameScene");
    }
}
