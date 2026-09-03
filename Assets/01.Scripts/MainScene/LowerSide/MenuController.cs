using System.Collections;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    //TODO 추후 개선 필요
    [SerializeField] private EquipmentInventoryManager _invenManager;
    [SerializeField] private EquipmentItemManager _equipManager;

    [SerializeField] private RectTransform _transform;
    private MenuButton[] _buttons;
    private MenuNumber _menuNumber;

    private Coroutine _moveCoroutine;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<MenuButton>();
    }

    private void Start()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].Init(i);
            _buttons[i].OnMenuButtonClicked += MoveMenuPos;

            //TODO 장비 불러오기 변경
            if (i == 1)
            {
                _buttons[i].OnButtonClicked += _invenManager.Init;
                _buttons[i].OnButtonClicked += _equipManager.Refresh;
            }
        }
    }

    public void MoveMenuPos(int number)
    {
        if ((int)_menuNumber == number)
            return;

        else if(_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = StartCoroutine(MoveMenuRoutine(number));
        }

        else
        {
            _moveCoroutine = StartCoroutine(MoveMenuRoutine(number));
        }
    }

    private IEnumerator MoveMenuRoutine(int number)
    {
        int moveSpeed = Mathf.Abs(number - (int)_menuNumber);
        _menuNumber = (MenuNumber)number;

        float timer = 0;

        Vector2 targetPos = new Vector2(number * -1080, 0);

        while(timer < 0.5)
        {
            timer += Time.deltaTime;

            _transform.anchoredPosition = Vector2.MoveTowards(_transform.anchoredPosition, targetPos, 3240 * moveSpeed * Time.deltaTime);

            yield return null;
        }

        _transform.anchoredPosition = targetPos;
    }
}