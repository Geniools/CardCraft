using System.Collections.ObjectModel;
using CardCraftClient.Service;
using CardCraftClient.View;
using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CardCraftClient.ViewModel;

public partial class DeckPageViewModel : BaseViewModel
{
    private SignalRService _signalRService;
    private readonly object _lock = new object();

    [ObservableProperty] 
    private ObservableCollection<IBaseCard> _availableCards;

    [ObservableProperty]
    private ObservableCollection<IBaseCard> _deck;

    [ObservableProperty] 
    private IBaseCard _selectedAvailableCard;

    [ObservableProperty] 
    private IBaseCard _selectedDeckCard;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAvailableCardsEnabled))]
    private int _deckCardsCount;

    public bool IsAvailableCardsEnabled => CanAddToDeck();

    // public Action AvailableCardsCollectionChangedAction;
    // public Action DeckCollectionChangedAction;

    public DeckPageViewModel(GameComponentsRegistration gcr, SignalRService signalRService)
    {
        this._signalRService = signalRService;

        AvailableCards = new ObservableCollection<IBaseCard>(gcr.Cards.OrderBy(c => c.ManaCost));
        Deck = new ObservableCollection<IBaseCard>();

        // Check if the player has a deck already
        if (this._signalRService.Player.Deck is not null)
        {
            // Reset the Deck
            this._signalRService.Player.Deck.Reset();

            // Get the cards from the player's deck
            Deck = new ObservableCollection<IBaseCard>(this._signalRService.Player.Deck.Cards);

            this.DeckCardsCount = Deck.Count;

            // Remove the cards from the available cards
            foreach (IBaseCard card in Deck)
            {
                AvailableCards.Remove(card);
            }
        }

        this.DeckCardsCount = Deck.Count;

        // Every time the available cards or the deck collection changes, sort the collections
        AvailableCards.CollectionChanged += (sender, args) =>
        {
            if (args.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                AvailableCards = new ObservableCollection<IBaseCard>(AvailableCards.OrderBy(c => c.ManaCost));
            }
        };

        Deck.CollectionChanged += (sender, args) =>
        {
            if (args.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Deck = new ObservableCollection<IBaseCard>(Deck.OrderBy(c => c.ManaCost));
            }
        };

        // Reset the data template when the collection changes - prevents a bug where the data template is not applied
        // This solution slows down the app A LOT (check the CardTemplateSelector for more information)

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

        lock (this._lock)
        {
            if (CanAddToDeck())
            {
                if (AvailableCards.Remove(card)) Deck.Add(card);
            }
        }

        this.DeckCardsCount = Deck.Count;

        IsBusy = false;
    }

    [RelayCommand]
    private async Task RemoveCardFromDeck(IBaseCard card)
    {
        IsBusy = true;

        lock (this._lock)
        {
            if (Deck.Remove(card)) AvailableCards.Add(card);
        }

        this.DeckCardsCount = Deck.Count;

        IsBusy = false;
    }

    [RelayCommand]
    private async Task CompleteRandomDeck()
    {
        IsBusy = true;

        var random = new Random();

        if (!CanAddToDeck())
        {
            IsBusy = false;
            return;
        }

        lock (this._lock)
        {
            // Add random cards to the rest of the deck
            for (int i = DeckCardsCount; i < DeckPool.MAX_AMOUNT_CARDS; i++)
            {
                int randomIndex = random.Next(0, AvailableCards.Count);
                Deck.Add(AvailableCards[randomIndex]);
                AvailableCards.RemoveAt(randomIndex);
            }
        }

        this.DeckCardsCount = Deck.Count;

        IsBusy = false;
    }

    [RelayCommand]
    private async Task SaveDeck()
    {
        IsBusy = true;

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
            // Create a DeckPool from the deck
            DeckPool deck = new(this.Deck);

            this._signalRService.Player.Deck = deck;
        }
        catch (Exception e)
        {
            Shell.Current.DisplayAlert("Error", e.Message, "OK");
            // Show error message
            return;
        }

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            // Must navigate to the StartPage first to reset the navigation stack and then navigate to the HeroPage
            // Otherwise, it will result in an error
            await Shell.Current.GoToAsync($"///{nameof(StartPage)}/{nameof(HeroPage)}");
        });

        IsBusy = false;
    }

    private bool CanAddToDeck()
    {
        return Deck.Count < DeckPool.MAX_AMOUNT_CARDS;
    }
}