using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageFloater : MonoBehaviour
{
    [SerializeField] private Image _criticalImage;
    [SerializeField] private TMP_Text _damageText;

    [SerializeField] private float _maxScale = 1.3f;
    [SerializeField] private float _upDuration = 0.3f;
    [SerializeField] private float _downDuration = 0.3f;

    private Color _redColor = Color.red;
    private Color _whiteColor = Color.white;

    private Vector3 _baseScale = Vector3.one * 0.01f;
    private Sequence _scaleSequence;

    public void InitDamageFloater(int damage, bool isCritical)
    {
        _scaleSequence?.Kill();
        gameObject.SetActive(true);
        transform.localScale = _baseScale;

        _criticalImage.gameObject.SetActive(isCritical);
        _damageText.color = isCritical ? _redColor : _whiteColor;
        _damageText.text = damage.ToAbbreviatedString();

        _scaleSequence = DOTween.Sequence()
            .Append(transform.DOScale(_baseScale * _maxScale, _upDuration))
            .Append(transform.DOScale(Vector3.zero, _downDuration))
            .OnComplete(() =>
            {
                _scaleSequence = null;
                PoolingObject poolOb = GetComponent<PoolingObject>();
                poolOb.ReturnToPool();
            });
    }
}
