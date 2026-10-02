using UnityEngine;

public class HeroSelectPresenter : MonoBehaviour
{
    [SerializeField] private HeroSelectViewer _viewer;
    [SerializeField] private HeroDetailPresenter _heroDetailPresenter;

    private HeroModel _heroModel;

    public void Awake()
    {
        _heroModel = GameManager.Instance.GameModel.HeroModel;

        if (!_heroModel.TryGetHeroConfigData(_heroModel.GetHeroId(), out SO_HeroConfigData data))
            return;

        _viewer.SetAnimController(data.HeroUIAnimController);

        _heroDetailPresenter.OnDetailClosed += _viewer.SetAnimController;
    }
}
