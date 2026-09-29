using UnityEngine;

public class HeroSelectPresenter : MonoBehaviour
{
    [SerializeField] private HeroSelectViewer _viewer;
    
    private HeroModel _heroModel;

    public void Awake()
    {
        _heroModel = GameManager.Instance.GameModel.HeroModel;

        _viewer.SetAnimController(_heroModel.GetHeroUIAnimController());
    }
}
