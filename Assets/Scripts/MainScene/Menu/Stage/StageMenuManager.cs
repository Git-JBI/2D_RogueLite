using UnityEngine;

public class StageMenuManager : MonoBehaviour
{
    private GameStartViewer _gameStartViewer;

    public void Awake()
    {
        _gameStartViewer = GetComponentInChildren<GameStartViewer>();
    }

    public void Start()
    {
        _gameStartViewer.OnGameStart += StartGame;
    }

    private bool CheckEnoughEnergy()
    {
        int energy = GameManager.Instance.EnergyManager.GetEnergy();

        return energy >= 5 ? true : false;
    }
    private void StartGame()
    {
        // 에너지가 충분할 경우
        if(CheckEnoughEnergy())
        {
            // 에너지 감소
            GameManager.Instance.EnergyManager.UseEnergy(5);

            // 게임 씬 불러오기
            GameManager.Instance.LoadSceneManager.LoadGameScene();
        }
    }
}
