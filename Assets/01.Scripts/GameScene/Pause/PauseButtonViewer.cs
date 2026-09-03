using System;
using UnityEngine.UI;

public class PauseButtonViewer : ButtonBase
{
    private Button _pauseButton;

    public event Action OnButtonClicked;

    private void Awake()
    {
        _pauseButton = GetComponent<Button>();

        _pauseButton.onClick.AddListener(() => OnButtonClicked?.Invoke());
    }
}
