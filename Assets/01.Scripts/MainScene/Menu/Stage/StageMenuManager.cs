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
        //TODO 0 => 5로 변경
        return GameManager.Instance.EnergyManager.Energy >= 0 ? true : false;
    }
    private void StartGame()
    {
        // 에너지가 충분할 경우
        if(CheckEnoughEnergy())
        {
            // 에너지 감소
            //TODO 0 => 5로 변경
            GameManager.Instance.EnergyManager.UseEnergy(0);

            // 게임 씬 불러오기
            GameManager.Instance.LoadSceneManager.LoadGameScene();
        }
    }
}
