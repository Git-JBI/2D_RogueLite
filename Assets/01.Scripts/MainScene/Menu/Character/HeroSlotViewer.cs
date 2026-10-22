using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HeroSlotViewer : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _heroImage;
    [SerializeField] private GameObject _selectedObject;
    [SerializeField] private GameObject _chooseImage;

    public event UnityAction OnButtonClicked;

    public void Awake()
    {
        _button.onClick.AddListener(HandleClick);
    }

    public void SetHeroImage(Sprite sprite)
    {
        _heroImage.sprite = sprite;
    }
    public void SetActiveSelectedObject(bool value)
    {
        _selectedObject.SetActive(value);
    }
    public void SetActiveChooseImage(bool value)
    {
        _chooseImage.SetActive(value);
    }

    private void HandleClick()
    {
        OnButtonClicked?.Invoke();
    }
}
