using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CardCraftClient.Model;
using CardCraftClient.Service;
using CardCraftShared.Core.Interfaces;
using CommunityToolkit.Mvvm.Input;

namespace CardCraftClient.ViewModel;

public partial class GamePageViewModel : BaseViewModel
{
    private readonly GameManager _gameManager;
    private readonly SignalRService _signalRService;

    [ObservableProperty] private int _timer;
    [ObservableProperty] private string _turnStatus;
    [ObservableProperty] private string _statusMessage;
    [ObservableProperty] private bool _isCurrentTurn;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(IsTemporaryCentredDisplayedCardVisible))] private BaseSpell _temporaryCentredDisplayedCard;

    partial void OnTemporaryCentredDisplayedCardChanged(BaseSpell? value)
    {
        // Display the card for 2 seconds
        if (value is not null)
        {
            Task.Delay(2000).ContinueWith(_ =>
            {
                this.TemporaryCentredDisplayedCard = null;
            });
        }
    }
    public bool IsTemporaryCentredDisplayedCardVisible => this.TemporaryCentredDisplayedCard is not null;

    // Current Player
    [ObservableProperty] private int _availableMana;
    [ObservableProperty] private Player _currentPlayer;
    [ObservableProperty] private int _currentPlayerHeroHealth;
    [ObservableProperty] private int _currentPlayerDeckCardCount;
    [ObservableProperty] private ObservableCollection<IBaseCard> _currentPlayerHand;
    [ObservableProperty] private IBaseCard _selectedHandCard;
    [ObservableProperty] private ObservableCollection<IMinion> _currentPlayerBoard;
    [ObservableProperty] private IBaseCard _selectedFriendlyBoardCard;

    // Enemy Player
    [ObservableProperty] private Player _enemyPlayer;
    [ObservableProperty] private int _enemyPlayerHeroHealth;
    [ObservableProperty] private int _enemyPlayerDeckCardCount;
    [ObservableProperty] private ObservableCollection<IBaseCard> _enemyPlayerHand;
    [ObservableProperty] private ObservableCollection<IMinion> _enemyPlayerBoard;
    [ObservableProperty] private IBaseCard _selectedEnemyBoardCard;

    public GamePageViewModel(GameManager gm, SignalRService signalRService)
    {
        this._gameManager = gm;
        this._signalRService = signalRService;

        this.Title = "Game";

        this.CurrentPlayer = gm.CurrentPlayer;
        this.EnemyPlayer = gm.EnemyPlayer;

        // Subscribe to game manager events
        gm.CurrentPlayer.Hand.Cards.CollectionChanged += async (sender, e) =>
        {
            this.CurrentPlayerHand = gm.CurrentPlayer.Hand.Cards;
            this.CurrentPlayerDeckCardCount = gm.CurrentPlayer.Deck.Cards.Count;

            // Make the message to be sent to the enemy player
            // Notify the server
            await this._signalRService.SendUpdateEnemyPlayer(new EnemyPlayerUpdateMessage()
            {
                HeroHealth = this.CurrentPlayerHeroHealth,
                PlayerHandCardAmount = this.CurrentPlayerHand.Count,
                PlayerDeckCardAmount = this.CurrentPlayerDeckCardCount
            });
        };

        gm.EnemyPlayer.Hand.Cards.CollectionChanged += (sender, e) =>
        {
            this.EnemyPlayerHand = gm.EnemyPlayer.Hand.Cards;
            this.EnemyPlayerDeckCardCount = gm.EnemyPlayer.Deck.Cards.Count;
        };

        gm.Board.FriendlySide.CollectionChanged += async (sender, e) =>
        {
            this.CurrentPlayerBoard = gm.Board.FriendlySide;
        };

        gm.Board.EnemySide.CollectionChanged += (sender, e) =>
        {
            this.EnemyPlayerBoard = gm.Board.EnemySide;
        };

        gm.CurrentPlayer.OnManaChanged += async (int newManaValue) =>
        {
            this.AvailableMana = newManaValue;
        };

        gm.OnTurnTimerChanged += async (int newTimerValue) =>
        {
            this.Timer = newTimerValue;
        };

        gm.OnCurrentTurnChanged += async (bool isCurrentTurn) =>
        {
            this.IsCurrentTurn = isCurrentTurn;

            this.TurnStatus = isCurrentTurn ? "Your Turn" : "Enemy's Turn";
        };

        gm.OnTemporaryCardDisplayAction += async (card) =>
        {
            this.TemporaryCentredDisplayedCard = card as BaseSpell;
        };

        // Set initial values
        this.CurrentPlayerHand = gm.CurrentPlayer.Hand.Cards;
        this.EnemyPlayerHand = gm.EnemyPlayer.Hand.Cards;

        this.CurrentPlayerBoard = gm.Board.FriendlySide;
        this.EnemyPlayerBoard = gm.Board.EnemySide;

        this.AvailableMana = gm.CurrentPlayer.Mana;

        this.CurrentPlayerHeroHealth = gm.CurrentPlayer.Hero.Health;
        this.EnemyPlayerHeroHealth = gm.EnemyPlayer.Hero.Health;

        this.CurrentPlayerDeckCardCount = gm.CurrentPlayer.Deck.Cards.Count;
        this.EnemyPlayerDeckCardCount = gm.EnemyPlayer.Deck.Cards.Count;

        // Send initial values to the server
        _ = this._signalRService.SendUpdateEnemyPlayer(new EnemyPlayerUpdateMessage()
        {
            HeroHealth = this.CurrentPlayerHeroHealth,
            PlayerHandCardAmount = this.CurrentPlayerHand.Count,
            PlayerDeckCardAmount = this.CurrentPlayerDeckCardCount
        });

        this.Timer = gm.TurnTimer;
        this.TurnStatus = gm.IsCurrentTurn ? "Your Turn" : "Enemy's Turn";
        this.IsCurrentTurn = gm.IsCurrentTurn;

        // Start the turn if it is the first turn
        if (gm.IsCurrentTurn)
        {
            _ = gm.NextTurn();
        }
    }

    [RelayCommand]
    private async Task EndTurn()
    {
        // End the turn by changing the current turn to false (do not call EndTurn() => it will create a new Thread which will behave unexpectedly afterwards)
        this._gameManager.IsCurrentTurn = false;
    }

    [RelayCommand]
    private async Task PlayCard()
    {
        IBaseCard card = this.SelectedHandCard;

        // Check if there is a card to be played
        if (card is null)
        {
            await Shell.Current.DisplayAlert("Error", "No card to be played selected", "OK");
            return;
        }

        // Check if the player has enough mana to play the card
        if (this.AvailableMana < card.ManaCost)
        {
            await Shell.Current.DisplayAlert("Error", "Not enough mana to play the card", "OK");
            return;
        }

        try
        {
            // Remove the card from the player's hand
            this._gameManager.CurrentPlayer.Hand.Remove(card);

            // Decrease the player's mana
            this.AvailableMana -= card.ManaCost;

            // If the card is a minion, play it on the board
            if (card is IMinion minionCard)
            {
                this._gameManager.Board.PlayMinionFriendlySide(minionCard);
            }
            else
            {
                // Display it as a temporary card in the center of the screen
                this.TemporaryCentredDisplayedCard = card as BaseSpell;
            }

            // Trigger the card effect
            card.TriggerEffect(this._gameManager.CurrentPlayer, this._gameManager.EnemyPlayer, this._gameManager.Board);

            // Notify the server
            await this._signalRService.SendPlayedCardToEnemyPlayer(card);
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Oops :(", e.Message, "OK");
        }
    }

    [RelayCommand]
    private async Task AttackMinion()
    {

    }

    [RelayCommand]
    private async Task AttackHero()
    {

    }


    private void LogAvailableCollections()
    {
        Trace.WriteLine("Current Player Hand:");
        foreach (var card in this.CurrentPlayerHand)
        {
            Trace.WriteLine(card.Name);
        }
        
        Trace.WriteLine("Enemy Player Hand:");
        foreach (var card in this.EnemyPlayerHand)
        {
            Trace.WriteLine(card.Name);
        }

        Trace.WriteLine("Current Player Board:");
        foreach (var card in this.CurrentPlayerBoard)
        {
            Trace.WriteLine(card.Name);
        }

        Trace.WriteLine("Enemy Player Board:");
        foreach (var card in this.EnemyPlayerBoard)
        {
            Trace.WriteLine(card.Name);
        }
    }
}
