using UnityEngine;

public class GachaManager : MonoBehaviour
{
    private Button_Gacha _button_Gacha;

    private void Awake()
    {
        _button_Gacha = GetComponentInChildren<Button_Gacha>();

        _button_Gacha.Init(this);
    }

    /// <summary>
    /// 장비 뽑기
    /// </summary>
    public void Gacha()
    {
        // GameManager.Instance.GameModel.GetItem();

        // 뽑을 종류
        // 등급, 등급에 따른 장비 종류, 갯수
    }
}
