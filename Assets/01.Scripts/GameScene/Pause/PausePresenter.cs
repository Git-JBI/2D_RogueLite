using UnityEngine;

public class PausePresenter : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private PauseButtonViewer _viewer;

    private void Start()
    {
        _viewer.OnButtonClicked += Pause;
    }

    private void Pause()
    {
        Time.timeScale = 0.0f;
        _pausePanel.SetActive(true);
    }
}
