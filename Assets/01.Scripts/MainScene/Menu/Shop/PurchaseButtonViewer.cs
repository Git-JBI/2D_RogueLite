using TMPro;
using UnityEngine;

public class PurchaseButtonViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private TextMeshProUGUI _requireText;

    public void Init(int require, int reward)
    {
        _requireText.text = require.ToString();
        _rewardText.text = reward.ToString();
    }
}
