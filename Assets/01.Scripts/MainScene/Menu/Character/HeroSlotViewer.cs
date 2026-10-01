using UnityEngine;
using UnityEngine.UI;

public class HeroSlotViewer : MonoBehaviour
{
    [SerializeField] private Image _heroImage;
    [SerializeField] private GameObject _selectedObject;
    [SerializeField] private GameObject _chooseImage;

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
}
