using UnityEngine;

public class HeroSelectPresenter : MonoBehaviour
{
    [SerializeField] private HeroSelectViewer _viewer;
    [SerializeField] private HeroDetailPresenter _heroDetailPresenter;

    private HeroModel _heroModel;

    public void Awake()
    {
        _heroModel = GameManager.Instance.GameModel.HeroModel;

        _viewer.SetAnimController(_heroModel.GetHeroUIAnimController());

        _heroDetailPresenter.OnDetailClosed += _viewer.SetAnimController;
    }
}
