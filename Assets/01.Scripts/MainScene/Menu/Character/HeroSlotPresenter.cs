using UnityEngine;

public class HeroSlotPresenter : MonoBehaviour
{
    [SerializeField] private HeroSlotViewer _viewer;
    private HeroSlotModel _model;

    public void Init(string heroId, bool check, Sprite heroPortraitSprite)
    {
        // 모델 생성
        _model = new(heroId);

        // 설정되있는 히어로인지 설정
        _model.SetIsSelected(check);
        _viewer.SetActiveSelectedObject(check);

        // 선택되있는 히어로인지 설정
        _model.SetIsChoosed(check);
        _viewer.SetActiveChooseImage(check);

        // 히어로 이미지 설정
        _viewer.SetHeroImage(heroPortraitSprite);
    }

    public bool GetIsSelected()
    {
        return _model.IsSelected;
    }
    public bool GetIsChoosed()
    {
        return _model.IsChoosed;
    }
}
