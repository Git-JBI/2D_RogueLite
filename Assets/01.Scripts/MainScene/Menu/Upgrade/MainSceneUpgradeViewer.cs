using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUpgradeViewer : MonoBehaviour
{
    private const string _maxLevelText = "Lv.Max";
    private const string _nonePriceText = "-";

    [Header("----- 글씨 색 -----")]
    [SerializeField] private Color _yellowColor;
    [SerializeField] private Color _skyblueColor;

    [Header("----- 버튼 -----")]
    [SerializeField] private Button _damageUpgradeButton;
    [SerializeField] private Button _hpUpgradeButton;
    [SerializeField] private Button _armorUpgradeButton;

    [Header("----- 데미지 증가-----")]
    [SerializeField] private TMP_Text _damageLevelText;
    [SerializeField] private TMP_Text _damagePriceText;

    [Header("----- 체력 -----")]
    [SerializeField] private TMP_Text _hpLevelText;
    [SerializeField] private TMP_Text _hpPriceText;

    [Header("----- 방어력 -----")]
    [SerializeField] private TMP_Text _armorLevelText;
    [SerializeField] private TMP_Text _armorPriceText;

    [Header("----- 오류 -----")]
    [SerializeField] private GameObject _warningPanel;
    [SerializeField] private TMP_Text _warningText;

    public event Action OnDamageUpgradeClicked;
    public event Action OnHpUpgradeClicked;
    public event Action OnArmorUpgradeClicked;

    private void Awake()
    {
        _damageUpgradeButton.onClick.AddListener(() => OnDamageUpgradeClicked?.Invoke());
        _hpUpgradeButton.onClick.AddListener(() => OnHpUpgradeClicked?.Invoke());
        _armorUpgradeButton.onClick.AddListener(() => OnArmorUpgradeClicked?.Invoke());
    }

    public void SetDamageButtonText(int level, int price)
    {
        // 최대 레벨일 경우
        if(price == -1)
        {
            _damageLevelText.text = _maxLevelText;
            _damagePriceText.text = _nonePriceText;
        }
        // 아닐 경우
        else
        {
            _damageLevelText.text = level.ToString();
            _damagePriceText.text = price.ToString();
        }
    }
    public void SetHpButtonText(int level, int price)
    {
        // 최대 레벨일 경우
        if (price == -1)
        {
            _hpLevelText.text = _maxLevelText;
            _hpPriceText.text = _nonePriceText;
        }
        // 아닐 경우
        else
        {
            _hpLevelText.text = level.ToString();
            _hpPriceText.text = price.ToString();
        }
    }
    public void SetArmorButtonText(int level, int price)
    {
        // 최대 레벨일 경우
        if (price == -1)
        {
            _armorLevelText.text = _maxLevelText;
            _armorPriceText.text = _nonePriceText;
        }
        // 아닐 경우
        else
        {
            _armorLevelText.text = level.ToString();
            _armorPriceText.text = price.ToString();
        }
    }

    /// <summary>
    /// 최대 레벨일 시 실행할 함수
    /// </summary>
    public void PlayMaxLevelWarning()
    {
        // 변할 색
        Color originColor = _skyblueColor;

        // 패널 활성화
        _warningPanel.SetActive(true);

        // 색 설정
        _warningText.color = originColor;

        // 문구 설정
        _warningText.text = "최대 레벨입니다.";

        // 시퀀스 생성
        Sequence sequence = DOTween.Sequence();

        // 0.5초간 알파값 0으로
        sequence.Append(_warningText.DOFade(0.0f, 0.5f));

        // 0.125초간 커졌다 작아졌다 4번
        sequence.Join(_warningText.transform.DOScale(1.2f, 0.125f).SetLoops(4, LoopType.Yoyo));

        // 실행 후
        sequence.OnComplete(() =>
        {
            // 색 원래대로
            _warningText.color = originColor;
            // 스케일 원래대로
            _warningText.transform.localScale = Vector3.one;
            // 패널 비활성화
            _warningPanel.SetActive(false);
        });
    }
    /// <summary>
    /// 골드가 부족할 시 실행할 함수
    /// </summary>
    public void PlayNotEnoughGoldWarning()
    {
        // 변할 색
        Color originColor = _yellowColor;

        // 패널 활성화
        _warningPanel.SetActive(true);

        // 색 설정
        _warningText.color = originColor;

        // 문구 설정
        _warningText.text = "골드가 부족합니다.";

        // 시퀀스 생성
        Sequence sequence = DOTween.Sequence();

        // 0.5초간 알파값 0으로
        sequence.Append(_warningText.DOFade(0.0f, 0.5f));

        // 0.125초간 커졌다 작아졌다 4번
        sequence.Join(_warningText.transform.DOScale(1.2f, 0.125f).SetLoops(4, LoopType.Yoyo));

        // 실행 후
        sequence.OnComplete(() =>
        {
            // 색 원래대로
            _warningText.color = originColor;
            // 스케일 원래대로
            _warningText.transform.localScale = Vector3.one;
            // 패널 비활성화
            _warningPanel.SetActive(false);
        });
    }
}
