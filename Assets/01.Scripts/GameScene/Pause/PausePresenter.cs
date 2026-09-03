using UnityEngine;

public class PausePresenter : MonoBehaviour
{
    [SerializeField] private GameSceneManager _gameSceneManager;
    [SerializeField] private PauseViewer _viewer;

    private void Start()
    {
        _viewer.OnButtonClicked += _gameSceneManager.Pause;
    }
}
