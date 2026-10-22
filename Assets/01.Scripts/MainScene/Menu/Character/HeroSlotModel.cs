public class HeroSlotModel
{
    private string _heroId;

    public string HeroId => _heroId;

    public HeroSlotModel(string heroId)
    {
        _heroId = heroId;
    }
}
