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
    private Color _cyanColor = Color.cyan;

    private Vector3 _baseScale = Vector3.one * 0.008f;
    private Sequence _scaleSequence;

    public void InitDamageFloater(int damage, bool isCritical)
    {
        _scaleSequence?.Kill();
        gameObject.SetActive(true);
        transform.localScale = _baseScale;

        // 크리티컬 이미지 활성화 여부
        _criticalImage.gameObject.SetActive(isCritical);

        // 텍스트 컬러 설정
        _damageText.color = isCritical ? _redColor : _cyanColor;

        // 텍스트 설정
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
