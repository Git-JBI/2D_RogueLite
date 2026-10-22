public class HeroDetailModel
{
    private string _selectedSlotId;
    private string _choosedSlotId;

    public string SelectedSlotId => _selectedSlotId;
    public string ChoosedSlotId => _choosedSlotId;

    public void SetSelectedSlotId(string id)
    {
        _selectedSlotId = id;
    }
    public void SetChoosedSlotId(string id)
    {
        _choosedSlotId = id;
    }
}
