using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuViewer : MonoBehaviour
{
    [SerializeField] private EquipmentInventoryManager _inventoryManager;
    [SerializeField] private Transform _menuTransform;
    private Button[] _menuButton;
    private MenuPresenter _presenter;

    public EquipmentInventoryManager InventoryManager => _inventoryManager;
    public Transform MenuTransfrom => _menuTransform;

    public UnityAction<int> OnMenuButtonClicked;

    private void Awake()
    {
        _menuButton = GetComponentsInChildren<Button>();
        _presenter = new MenuPresenter(this);

        for(int i = 0; i < _menuButton.Length; i++)
        {
            int index = i;
            _menuButton[i].onClick.AddListener(() => _presenter.MoveMenuPos(index));
        }
    }
}