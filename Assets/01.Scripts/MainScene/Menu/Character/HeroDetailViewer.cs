using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HeroDetailViewer : MonoBehaviour
{
    [SerializeField] private Button _backButton;

    public event UnityAction OnBackButtonClicked;

    private void OnEnable()
    {
        OnBackButtonClicked += ClosePanel;

        _backButton.onClick.AddListener(OnBackButtonClick);
    }
    private void OnDisable()
    {
        OnBackButtonClicked -= ClosePanel;

        _backButton.onClick.RemoveAllListeners();
    }

    private void OnBackButtonClick()
    {
        OnBackButtonClicked?.Invoke();
    }

    private void ClosePanel()
    {
        gameObject.transform.DOScale(Vector3.zero, 0.2f)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
