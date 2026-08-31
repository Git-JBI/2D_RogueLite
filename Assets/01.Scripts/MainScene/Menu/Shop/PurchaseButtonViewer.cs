using TMPro;
using UnityEngine;

public class PurchaseButtonViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private TextMeshProUGUI _requireText;

    public void Init(int require, int reward)
    {
        _requireText.text = _requireText.ToString();
        _rewardText.text = _rewardText.ToString();
    }
}
