using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewer : MonoBehaviour
{
    private Image _gradeImage;
    private TextMeshProUGUI _descText;

    private void Awake()
    {
        _gradeImage = GetComponentInChildren<Image>();
        _descText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Init(int gradeValue, int desc)
    {
        SetGradeColor(gradeValue);
        SetDescText(desc);
    }

    private void SetGradeColor(int value)
    {
        switch(value)
        {
            case 1:
                _gradeImage.color = Color.gray;
                break;
            case 2:
                _gradeImage.color = Color.green;
                break;
            case 3:
                _gradeImage.color = Color.blue;
                break;
            case 4:
                _gradeImage.color = Color.magenta;
                break;
            case 5:
                _gradeImage.color = Color.yellow;
                break;
            case 6:
                _gradeImage.color = Color.red;
                break;
        }
    }

    private void SetDescText(int value)
    {
        _descText.text = value.ToString();
    }
}
