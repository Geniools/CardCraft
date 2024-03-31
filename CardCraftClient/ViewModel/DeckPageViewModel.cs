using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CardCraftClient.Service;
using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CardCraftClient.ViewModel;

public partial class DeckPageViewModel : BaseViewModel
{
    private SignalRService _signalRService;

    [ObservableProperty] 
    private ObservableCollection<IBaseCard> _availableCards;

    [ObservableProperty]
    private ObservableCollection<IBaseCard> _deck;

    [ObservableProperty] 
    private IBaseCard _selectedAvailableCard;

    [ObservableProperty] 
    private IBaseCard _selectedDeckCard;

    [ObservableProperty]
    private int _deckCardsCount;

    public Action AvailableCardsCollectionChangedAction;
    public Action DeckCollectionChangedAction;

    public DeckPageViewModel(GameComponentsRegistration gcr, SignalRService signalRService)
    {
        this._signalRService = signalRService;

        AvailableCards = new ObservableCollection<IBaseCard>(gcr.Cards);
        Deck = new ObservableCollection<IBaseCard>();

        this.DeckCardsCount = Deck.Count;

        // Reset the data template when the collection changes - prevents a bug where the data template is not applied
        // This solution slows down the app A LOT, so it's commented out

        // AvailableCards.CollectionChanged += (sender, args) =>
        // {
        //     if (args.Action == NotifyCollectionChangedAction.Add) AvailableCardsCollectionChangedAction?.Invoke();
        // };
        //
        // Deck.CollectionChanged += (sender, args) =>
        // {
        //     if (args.Action == NotifyCollectionChangedAction.Add) DeckCollectionChangedAction?.Invoke();
        // };
    }

    [RelayCommand]
    private async Task AddCardToDeck(IBaseCard card)
    {
        IsBusy = true;

        if (Deck.Count > 30)
        {
            Shell.Current.DisplayAlert("Error", "Deck is full!", "OK");

            IsBusy = false;
            // Show error message
            return;
        }

        if (AvailableCards.Remove(card)) Deck.Add(card);
        this.DeckCardsCount = Deck.Count;

        IsBusy = false;
    }

    [RelayCommand]
    private async Task RemoveCardFromDeck(IBaseCard card)
    {
        IsBusy = true;

        if (Deck.Remove(card)) AvailableCards.Add(card);
        this.DeckCardsCount = Deck.Count;

        IsBusy = false;
    }

    [RelayCommand]
    private async Task SaveDeck()
    {
        // Validate the deck
        if (Deck.Count < 10)
        {
            Shell.Current.DisplayAlert("Error", "Deck must contain at least 10 cards!", "OK");
            // Show error message
            return;
        }

        // Save the deck
        try
        {
            // TODO: Create a deck object
            this._signalRService.Player.Deck.AddDeck(this.Deck);
        }
        catch (Exception e)
        {
            Shell.Current.DisplayAlert("Error", e.Message, "OK");
            // Show error message
            return;
        }

        MainThread.BeginInvokeOnMainThread(() =>
        {
            Shell.Current.GoToAsync("..");
        });
    }
}