using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HeroDetailViewer : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text _heroNameText;
    [SerializeField] private TMP_Text _heroDescText;

    [SerializeField] private Button _backButton;
    [SerializeField] private Button _selectButton;

    public event UnityAction OnBackButtonClicked;
    public event UnityAction OnSelectButtonClicked;

    public void Awake()
    {
        OnBackButtonClicked += ClosePanel;

        _backButton.onClick.AddListener(OnBackButtonClick);
        _selectButton.onClick.AddListener(OnSelectButtonClick);
    }

    /// <summary>
    /// AnimController 설정 함수
    /// </summary>
    /// <param name="animController"></param>
    public void SetAnimController(RuntimeAnimatorController animController)
    {
        _animator.runtimeAnimatorController = animController;
    }
    /// <summary>
    /// CharacterName 설정 함수
    /// </summary>
    /// <param name="name"></param>
    public void SetHeroNameText(string name)
    {
        _heroNameText.text = name;
    }
    /// <summary>
    /// CharacterDesc 설정 함수
    /// </summary>
    /// <param name="desc"></param>
    public void SetHeroDescText(string desc)
    {
        _heroDescText.text = desc;
    }
    /// <summary>
    /// SelectButton활성화 여부 함수
    /// </summary>
    /// <param name="value"></param>
    public void SetActiveSelectButton(bool value)
    {
        _selectButton.gameObject.SetActive(value);
    }

    private void OnBackButtonClick()
    {
        OnBackButtonClicked?.Invoke();
    }
    private void OnSelectButtonClick()
    {
        OnSelectButtonClicked?.Invoke();
    }
    private void ClosePanel()
    {
        gameObject.transform.DOScale(Vector3.zero, 0.1f)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
