using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Warning : MonoBehaviour
{
    [SerializeField] private Image _warningImage;
    [SerializeField] private TextMeshProUGUI _warningText;

    public void BeforeBurstSpawn()
    {
        // 활성화
        gameObject.SetActive(true);

        // 텍스트 설정
        _warningText.text = "적이 몰려옵니다!";

        OpenPanel();

        // 이미지 크기 애니메이션 실행
        PlayWarningAnimation();
    }
    public void BeforeBossSpawn()
    {
        // 활성화
        gameObject.SetActive(true);

        // 텍스트 설정
        _warningText.text = "보스가 등장합니다.";

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
