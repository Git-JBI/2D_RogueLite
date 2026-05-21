using System.Text;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    private Button_Gacha[] _button_Gachas;

    //TODO 추후 UI쪽에서 받아오기
    [SerializeField] private GetItemViewer _itemViewer;

    private void Awake()
    {
        _button_Gachas = GetComponentsInChildren<Button_Gacha>();

        foreach(Button_Gacha x in _button_Gachas)
        {
            x.Init();
        }

        _button_Gachas[0].OnButtonClicked += Gacha;
        _button_Gachas[1].OnButtonClicked += Gacha10;
    }
    /*
     * 각 종류별 뽑기에 대한 등급별 확률로 등급 결정
       ->
       설정 데이터 딕셔너리에서 뽑힌 등급에 해당하는 아이템 아이디 리스트
       ->
       그중에서 랜덤하게 하나
     */

    /// <summary>
    /// 단일 장비 뽑기
    /// </summary>
    public void Gacha()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Equipment");
        // 장비 종류 선택(1 ~ 6)
        //itemKey += 10000 * Random.Range(1, 7);

        /*
         * 
         */
        // 등급 선택
        //int check = Random.Range(1, 101);
        //if (check <= 60)
        //    itemKey += 1000 * 1;
        //else if (check <= 90)
        //    itemKey += 1000 * 2;
        //else if (check <= 99)
        //    itemKey += 1000 * 3;
        //else
        //    itemKey += 1000 * 4;
        //
        //// 세부 설정
        //itemKey += 1 * Random.Range(1, 7);

        string itemId = sb.ToString();

        // 아이템 저장
        GameManager.Instance.GameModel.GetEquipItem(itemId);

        // 획득한 아이템 보여주기
        _itemViewer.ShowOne(itemId);
    }

    public void Gacha10()
    {
        // 10번 뽑기용도 itemKey 배열
        int[] itemKeys = new int[10];

        // 기본 값 설정
        for(int i = 0; i < itemKeys.Length; i++)
        {
            itemKeys[i] = 011000000;

            // 장비 종류 선택(1 ~ 6)
            itemKeys[i] += 10000 * Random.Range(1, 7);

            // 등급 선택
            int check = Random.Range(1, 101);
            if (check <= 60)
                itemKeys[i] += 1000 * 1;
            else if (check <= 90)
                itemKeys[i] += 1000 * 2;
            else if (check <= 99)
                itemKeys[i] += 1000 * 3;
            else
                itemKeys[i] += 1000 * 4;

            // 세부 설정
            itemKeys[i] += 1 * Random.Range(1, 7);

            // 아이템 저장
            // GameManager.Instance.GameModel.GetEquipItem(itemKeys[i]);
        }

        // 획득한 아이템 보여주기
        _itemViewer.ShowTen(itemKeys);
    }

    /// <summary>
    /// 아이템의 등급을 결정하는 함수
    /// </summary>
    private void DetermineRarity()
    {

    }
}
