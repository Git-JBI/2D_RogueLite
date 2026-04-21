using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countDeadEnemiesText;

    public void IncreaseDeadCount(int count)
    {
        _countDeadEnemiesText.text = count.ToString();
    }
}
