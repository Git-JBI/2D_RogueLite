using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyViewerBase : MonoBehaviour
{
    [Header("----- 부모 설정 -----")]
    [SerializeField] private Transform _canvas;

    [Header("----- 스폰 반지름 -----")]
    [SerializeField] private float _radius;

    [Header("----- 타겟 위치 -----")]
    [SerializeField] private Transform _targetPos;

    [Header("----- 이미지 프리팹 -----")]
    [SerializeField] private GameObject _prefab;

    [Header("----- 버튼 -----")]
    [SerializeField] private Button[] _buttons;

    [Header("----- 텍스트 -----")]
    [SerializeField] private TMP_Text[] _requireText;
    [SerializeField] private TMP_Text[] _rewardText;

    public event UnityAction<int> OnButtonsClicked; 

    private void Awake()
    {
        for(int i = 0; i < _buttons.Length; i++)
        {
            int index = i;
            _buttons[i].onClick.AddListener(() => OnButtonsClicked?.Invoke(index));
            _buttons[i].onClick.AddListener(() => SpawnRewards(index));
        }
    }

    public void SetRewardText(int index, int reward)
    {
        _rewardText[index].text = reward.ToString();
    }
    public void SetRequireText(int index, int require)
    {
        _requireText[index].text = require.ToString();
    }

    private void SpawnRewards(int index)
    {
        int count = (index + 1) * 5;
        for(int i = 0; i < count; i++)
        {
            GameObject emerald = Instantiate(_prefab, _buttons[index].transform.position, Quaternion.identity, _buttons[index].transform);
            emerald.transform.SetParent(_canvas);
            Vector3 randomPos = Random.insideUnitCircle * _radius;
            Sequence sec = DOTween.Sequence();
            sec.Append(emerald.transform.DOJump(_buttons[index].transform.position + randomPos, 2, 1, 0.3f));
            sec.AppendInterval(0.3f);
            sec.Append(emerald.transform.DOMove(_targetPos.position, 0.5f));
            sec.OnComplete(() => Destroy(emerald));
        }
    }
}
