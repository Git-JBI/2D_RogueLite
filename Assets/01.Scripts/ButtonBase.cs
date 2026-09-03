using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBase : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler, IPointerUpHandler
{
    private bool _isClicked;

    private void ScaleDown()
    {
        transform.DOScale(0.9f, 0.2f);
    }

    private void ResetScale()
    {
        transform.DOScale(1.0f, 0.2f);
    }

    #region PointerInterface
    public void OnPointerUp(PointerEventData eventData)
    {
        _isClicked = false;

        ResetScale();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isClicked = true;

        ScaleDown();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetScale();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isClicked)
            ScaleDown();
    }
    #endregion
}
