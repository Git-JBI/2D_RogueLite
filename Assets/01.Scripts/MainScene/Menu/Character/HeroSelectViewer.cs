using DG.Tweening;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HeroSelectViewer : MonoBehaviour
{
    [SerializeField] private Button _heroSelectButton;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _heroDetailGameObject;

    public event UnityAction OnButtonClicked;

    public void Awake()
    {
        _heroSelectButton.onClick.AddListener(() =>
        {
            OnButtonClicked?.Invoke();
            _heroDetailGameObject.SetActive(true);
            _heroDetailGameObject.transform.localScale = Vector3.zero;
            _heroDetailGameObject.transform.DOScale(Vector3.one, 0.1f);
        });
    }

    public void SetAnimController(AnimatorController animController)
    {
        _animator.runtimeAnimatorController = animController;
    }
}
