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

    private Sequence _warningSequence;

    public event Action<UpgradeType> OnUpgradeClicked;

    private void Awake()
    {
        _damageUpgradeButton.onClick.AddListener(() => OnUpgradeClicked?.Invoke(UpgradeType.Damage));
        _hpUpgradeButton.onClick.AddListener(() => OnUpgradeClicked?.Invoke(UpgradeType.Hp));
        _armorUpgradeButton.onClick.AddListener(() => OnUpgradeClicked?.Invoke(UpgradeType.Armor));
    }

    private void OnDestroy()
    {
        _warningSequence?.Kill();
        _warningSequence = null;
    }

    /// <summary>
    /// type에 따른 level, price 설정 함수
    /// </summary>
    /// <param name="type"></param>
    /// <param name="level"></param>
    /// <param name="price"></param>
    public void SetButtonText(UpgradeType type, int level, int price)
    {
        switch (type)
        {
            case UpgradeType.Damage:
                SetText(_damageLevelText, _damagePriceText, level, price);
                break;

            case UpgradeType.Hp:
                SetText(_hpLevelText, _hpPriceText, level, price);
                break;

            case UpgradeType.Armor:
                SetText(_armorLevelText, _armorPriceText, level, price);
                break;
        }
    }

    /// <summary>
    /// 최대 레벨일 시 실행할 함수
    /// </summary>
    public void PlayMaxLevelWarning()
    {
        PlayWarning("최대 레벨입니다.", _skyblueColor);
    }
    /// <summary>
    /// 골드가 부족할 시 실행할 함수
    /// </summary>
    public void PlayNotEnoughGoldWarning()
    {
        PlayWarning("골드가 부족합니다.", _yellowColor);
    }

    /// <summary>
    /// 경고 실행 함수
    /// </summary>
    /// <param name="message"></param>
    /// <param name="color"></param>
    private void PlayWarning(string message, Color color)
    {
        // 이전 시퀀스를 종료(완료 콜백 X)
        _warningSequence?.Kill();

        // 이전 애니메이션의 상태 초기화
        _warningText.color = color;
        _warningText.transform.localScale = Vector3.one;
        _warningText.text = message;
        _warningPanel.SetActive(true);

        _warningSequence = DOTween.Sequence();

        // 1초간 알파값 0으로
        _warningSequence.Append(_warningText.DOFade(0.0f, 1.0f));

        // 0.25초간 커졌다 작아졌다 4번
        _warningSequence.Join(_warningText.transform.DOScale(1.2f, 0.25f).SetLoops(4, LoopType.Yoyo));

        _warningSequence.OnComplete(() =>
        {
            _warningText.color = color;
            _warningText.transform.localScale = Vector3.one;
            _warningPanel.SetActive(false);
            _warningSequence = null;
        });
    }
    /// <summary>
    /// level, price Text설정 함수
    /// </summary>
    /// <param name="levelText"></param>
    /// <param name="priceText"></param>
    /// <param name="level"></param>
    /// <param name="price"></param>
    private void SetText(TMP_Text levelText, TMP_Text priceText, int level, int price)
    {
        bool isMaxLevel = price == -1;

        levelText.text = isMaxLevel ? _maxLevelText : level.ToString();
        priceText.text = isMaxLevel ? _nonePriceText : price.ToString();
    }
}
