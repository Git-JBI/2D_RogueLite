using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SpawnWarningType
{
    Wave,
    Boss,
}

public class WarningEffectViewer : MonoBehaviour
{
    [SerializeField] private Image _warningImage;
    [SerializeField] private TMP_Text _warningText;

    public void ShowWarning(SpawnWarningType type)
    {
        // 텍스트 설정
        switch (type)
        {
            case SpawnWarningType.Wave:
                _warningText.text = "적들이 몰려옵니다!";
                break;

            case SpawnWarningType.Boss:
                _warningText.text = "보스가 등장합니다!";
                break;

            default:
                Debug.LogWarning($"지원하지 않는 경고 타입: {type}");
                return;
        }

        // 경고 오브젝트 활성화
        gameObject.SetActive(true);

        // 패널 열기
        OpenPanel();

        // 이미지 크기 애니메이션 실행
        PlayWarningAnimation();
    }

    private void OpenPanel()
    {
        // 크기 0으로 설정
        transform.localScale = Vector3.zero;

        // 본 크기로 0.5초동안 늘어나도록 설정
        transform.DOScale(1, 0.5f);
    }

    private void PlayWarningAnimation()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(_warningImage.transform.DOScale(1.5f, 0.5f));
        sequence.Join(_warningImage.DOColor(Color.yellow, 0.5f));

        sequence.Append(_warningImage.transform.DOScale(1.0f, 0.5f));
        sequence.Join(_warningImage.DOColor(Color.white, 0.5f));

        // 2번 실행
        sequence.SetLoops(2);

        // sequence 다 끝난 후 실행
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
