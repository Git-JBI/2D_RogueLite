using UnityEngine;
using UnityEngine.EventSystems;

public class GetItemViewer : MonoBehaviour, IPointerClickHandler
{
    private ItemViewer[] _viewers;

    private void Awake()
    {
        _viewers = GetComponentsInChildren<ItemViewer>();

        foreach(ItemViewer x in _viewers)
        {
            x.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    //TODO string수정 필요
    public void ShowOne(string itemid)
    {
        gameObject.SetActive(true);
        _viewers[0].gameObject.SetActive(true);
    }

    //TODO string수정 필요
    public void ShowTen(int[] keys)
    {
        gameObject.SetActive(true);

        for(int i = 0; i < _viewers.Length; i++)
        {
            _viewers[i].gameObject.SetActive(true);

            int grade = (keys[i] / 1000) % 10;
            int desc = keys[i] % 100;

            _viewers[i].Init(grade, desc);
        }
    }

    private void Close()
    {
        for(int i = 0; i < _viewers.Length; i++)
        {
            _viewers[i].gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Close();
    }
}
