using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countDeadEnemiesText;

    private int _deadCount;

    private void Awake()
    {
        _deadCount = 0;
    }

    public void IncreaseDeadCount()
    {
        _deadCount++;
        _countDeadEnemiesText.text = _deadCount.ToString();
    }
}
