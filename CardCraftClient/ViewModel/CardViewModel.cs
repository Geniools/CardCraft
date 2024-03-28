using System.ComponentModel;
using CardCraftShared.Cards.Minions;

namespace CardCraftClient.ViewModel;

public partial class CardViewModel : BaseViewModel
{
    private AlexCard _card;
    public AlexCard Card
    {
        get => _card;
        set
        {
            _card = value;
            OnPropertyChanged(nameof(Card));
        }
    }

    public CardViewModel()
    {
        Card = new AlexCard(); // Initialize your card with default values or load from somewhere
    }

    // Implement commands or methods here if needed

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}