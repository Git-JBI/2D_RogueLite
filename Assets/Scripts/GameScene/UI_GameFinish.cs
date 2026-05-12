using TMPro;
using UnityEngine;

public class UI_GameFinish : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;

    public void Clear()
    {
        // 성공 텍스트
        _titleText.text = "클리어 성공";
    }

    public void Fail()
    {
        // 실패 텍스트
        _titleText.text = "클리어 실패";
    }
}
