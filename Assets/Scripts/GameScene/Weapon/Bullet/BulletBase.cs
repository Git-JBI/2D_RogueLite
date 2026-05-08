using UnityEngine;

public class BulletBase : MonoBehaviour
{
    //TODO BulletModel or Data를 따로 만들어서 플레이어, 적 양쪽 다 사용 가능하도록 변경
    protected HeroModel _model;
    protected float _damageMagnification;
    protected string _findObjectTag;

    //TODO 초기화 시점에서 태그(string)를 받아 감지 조건 판단
    public void Init(HeroModel model, float damageMagnification)
    {
        _model = model;
        _damageMagnification = damageMagnification;
    }
}