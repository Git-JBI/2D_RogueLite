using UnityEngine;
using UnityEngine.UI;

public class ToggleViewer : MonoBehaviour
{
    [Header("----- 바뀔 스프라이트 -----")]
    [SerializeField] private Sprite[] _sprites;
    [Header("----- 이미지 컴포넌트 -----")]
    [SerializeField] private Image _image;

    public void Init(bool value)
    {
        SetImage(value);
    }

    private void SetImage(bool value)
    {
        if (value)
            _image.sprite = _sprites[0];
        else
            _image.sprite = _sprites[1];
    }
}
