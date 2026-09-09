using System;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentGachaViewer : MonoBehaviour
{
    [Header("----- 패널 -----")]
    [SerializeField] private GameObject _gachaResultPanel;

    [Header("----- 버튼 -----")]
    [SerializeField] private Button _equipmentGacha1xButton;
    [SerializeField] private Button _equipmentGacha10xButton;
    [SerializeField] private Button _equipmentGacha50xButton;

    public event Action<EquipmentGachaType> OnEquipmentGachaButtonClicked;

    private void Awake()
    {
        _equipmentGacha1xButton.onClick.AddListener(() => OnEquipmentGachaButtonClicked?.Invoke(EquipmentGachaType.Single));
        _equipmentGacha10xButton.onClick.AddListener(() => OnEquipmentGachaButtonClicked?.Invoke(EquipmentGachaType.Ten));
        _equipmentGacha50xButton.onClick.AddListener(() => OnEquipmentGachaButtonClicked?.Invoke(EquipmentGachaType.Fifty));
    }

    // public void Gacha(EquipmentGachaType type)
    // {
    //     OnEquipmentGachaButtonClicked?.Invoke()
    // }
}
