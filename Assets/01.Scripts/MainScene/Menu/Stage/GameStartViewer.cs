using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameStartViewer : MonoBehaviour, IPointerDownHandler
{
    private Button _button;
    private Sequence _pointerDownSequence;

    public event UnityAction OnGameStart;

    public void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(GameStart);

        _pointerDownSequence = DOTween.Sequence()
            .Append(transform.DOScale(1.2f, 0.2f))
            .Append(transform.DOScale(1.0f, 0.2f))
            .SetAutoKill(false)
            .Pause();
    }

    public void OnDestroy()
    {
        _pointerDownSequence.Kill();
    }

    private void GameStart()
    {
        OnGameStart?.Invoke();
    }

    #region Interface
    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDownSequence.Restart();
    }
    #endregion
}
