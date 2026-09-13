using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GachaItemViewer : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _rarityImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image[] _spaklingImage;

    [Header("----- 설정 -----")]
    [SerializeField] private float _minScale;
    [SerializeField] private float _maxScale;
    [SerializeField] private float _scaleSpeed;

    private Coroutine _rotateRoutine;
    private Coroutine _sparkleRoutine;

    public void SetRaritySprite(Sprite sprite)
    {
        _rarityImage.sprite = sprite;
    }

    public void SetItemSprite(Sprite sprite)
    {
        _itemImage.sprite = sprite;
    }

    public void GetSpecialItem()
    {
        Debug.Log("유니크 이상!");

        _backgroundImage.gameObject.SetActive(true);
        foreach(Image sparkles in _spaklingImage)
        {
            sparkles.gameObject.SetActive(true);
        }

        if (_rotateRoutine != null)
        {
            StopCoroutine(_rotateRoutine);
            StopCoroutine(_sparkleRoutine);
        }

        _rotateRoutine = StartCoroutine(RotateBackgroundRoutine());
        _sparkleRoutine = StartCoroutine(SparkleScaleRoutine());
    }
    public void TurnOffEffect()
    {
        if (_rotateRoutine != null)
        {
            StopCoroutine(_rotateRoutine);
            StopCoroutine(_sparkleRoutine);
        }

        _backgroundImage.gameObject.SetActive(false);

        foreach (Image sparkles in _spaklingImage)
        {
            sparkles.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 배경 후광 돌아가는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotateBackgroundRoutine()
    {
        while(true)
        {
            _backgroundImage.transform.Rotate(Vector3.forward * 0.5f);
            yield return null;
        }
    }

    private IEnumerator SparkleScaleRoutine()
    {
        float t = 0f;

        while (true)
        {
            t += Time.deltaTime * _scaleSpeed;

            // 0 ~ 1 사이를 부드럽게 왕복
            float lerpValue = (Mathf.Sin(t) + 1f) * 0.5f;

            float scale = Mathf.Lerp(_minScale, _maxScale, lerpValue);

            foreach (Image sparkle in _spaklingImage)
            {
                if (sparkle == null)
                    continue;

                sparkle.transform.localScale =
                    new Vector3(scale, scale, 1f);
            }

            yield return null;
        }
    }
}
