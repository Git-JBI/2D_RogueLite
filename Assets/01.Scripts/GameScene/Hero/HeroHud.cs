using UnityEngine;
using UnityEngine.UI;

public class HeroHud : MonoBehaviour
{
    [SerializeField] private Image _hpBar;

    public void SetHpBar(float curHp, float maxHp)
    {
        _hpBar.fillAmount = curHp / maxHp;
    }
}
