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
        _equipmentGacha1xButton.onClick.AddListener(() => OnEquipmentGachaButtonClicked?.Invoke(EquipmentGachaType.Ten));
        _equipmentGacha1xButton.onClick.AddListener(() => OnEquipmentGachaButtonClicked?.Invoke(EquipmentGachaType.Fifty));
    }

    public void Gacha(EquipmentGachaType type)
    {
        switch(type)
        {
            case EquipmentGachaType.Single:
                Debug.Log("한 번");
                break;
            case EquipmentGachaType.Ten:
                Debug.Log("열 번");
                break;
            case EquipmentGachaType.Fifty:
                Debug.Log("오십 번");
                break;
        }
    }
}
