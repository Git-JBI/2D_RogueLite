using UnityEditor.Animations;

public class HeroSlotModel
{
    private string _heroId;

    private bool _isSelected;
    private bool _isChoosed;

    public string HeroId => _heroId;

    public bool IsSelected => _isSelected;
    public bool IsChoosed => _isChoosed;

    public HeroSlotModel(string heroId)
    {
        _heroId = heroId;
    }

    public void SetIsSelected(bool value)
    {
        _isSelected = value;
    }
    public void SetIsChoosed(bool value)
    {
        _isChoosed = value;
    }
}
