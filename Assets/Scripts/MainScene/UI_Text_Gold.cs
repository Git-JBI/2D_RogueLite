using TMPro;
using UnityEngine;

public class UI_Text_Gold : MonoBehaviour
{
    private TextMeshProUGUI _goldText;

    private void Awake()
    {
        _goldText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateGoldText(int goldAmount)
    {
        _goldText.text = goldAmount.ToAbbreviatedString();
    }
}