using System;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentGachaViewer : MonoBehaviour
{
    [SerializeField] private Button _oneGachaButton;
    [SerializeField] private Button _tenGachaButton;

    public event Action OnOneGachaButtonClicked;
    public event Action OnTenGachaButtonClicked;

    private void Awake()
    {
        _oneGachaButton.onClick.AddListener(() => OnOneGachaButtonClicked?.Invoke());
        _tenGachaButton.onClick.AddListener(() => OnTenGachaButtonClicked?.Invoke());
    }
}
