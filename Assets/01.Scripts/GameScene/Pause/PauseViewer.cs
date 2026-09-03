using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseViewer : ButtonBase
{
    [SerializeField] private Button _pauseButton;

    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _homeButton;

    public event Action OnButtonClicked;

    private void Awake()
    {
        _pauseButton.onClick.AddListener(() =>
        {
            OnButtonClicked?.Invoke();
            _pausePanel.SetActive(true);
        });
    }
}
