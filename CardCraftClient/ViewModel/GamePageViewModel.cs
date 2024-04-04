using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CardCraftClient.Model;
using CardCraftClient.Service;
using CommunityToolkit.Mvvm.Input;

namespace CardCraftClient.ViewModel;

public partial class GamePageViewModel : BaseViewModel
{
    private GameManager _gameManager;
    private SignalRService _signalRService;

    [ObservableProperty] private int _timer;
    [ObservableProperty] private string _statusMessage;

    // Current Player
    [ObservableProperty] private int _availableMana;
    [ObservableProperty] private Player _currentPlayer;
    [ObservableProperty] private int _currentPlayerHeroHealth;
    [ObservableProperty] private int _currentPlayerDeckCardCount;
    [ObservableProperty] private ObservableCollection<IBaseCard> _currentPlayerHand;
    [ObservableProperty] private IBaseCard _selectedHandCard;
    [ObservableProperty] private ObservableCollection<IBaseCard> _currentPlayerBoard;
    [ObservableProperty] private IBaseCard _selectedFriendlyBoardCard;

    // Enemy Player
    [ObservableProperty] private Player _enemyPlayer;
    [ObservableProperty] private int _enemyPlayerHeroHealth;
    [ObservableProperty] private int _enemyPlayerDeckCardCount;
    [ObservableProperty] private ObservableCollection<IBaseCard> _enemyPlayerHand;
    [ObservableProperty] private ObservableCollection<IBaseCard> _enemyPlayerBoard;
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
        this._signalRService.SendUpdateEnemyPlayer(new EnemyPlayerUpdateMessage()
        {
            HeroHealth = this.CurrentPlayerHeroHealth,
            PlayerHandCardAmount = this.CurrentPlayerHand.Count,
            PlayerDeckCardAmount = this.CurrentPlayerDeckCardCount
        });

        gm.OnTurnTimerChanged += async (int newTimer) =>
        {
            this.Timer = newTimer;
        };

        gm.OnCurrentTurnChanged += async (bool isCurrentTurn) =>
        {
            Trace.WriteLine($"TURN CHANGED: {isCurrentTurn}. PLAYER: {this._signalRService.Player.Name}");
            if (isCurrentTurn)
            {
                this.StatusMessage = "Your Turn";
            }
            else
            {
                this.StatusMessage = "Enemy Turn";
            }
        };

        this.Timer = gm.TurnTimer;
        this.StatusMessage = gm.IsCurrentTurn ? "Your Turn" : "Enemy Turn";

        if (gm.IsCurrentTurn)
        {
            _ = gm.NextTurn();
        }
    }

    [RelayCommand]
    private async Task EndTurn()
    {
        await this._gameManager.EndTurn();
    }

    [RelayCommand]
    private async Task PlayCard()
    {
        IBaseCard card = this.SelectedHandCard;

        // Check if there is a card to be played
        if (this.SelectedHandCard is null)
        {
            await Shell.Current.DisplayAlert("Error", "No card to be played selected", "OK");
            return;
        }

        // Check if the player has enough mana to play the card
        // if (this.AvailableMana < this.SelectedHandCard.ManaCost)
        // {
        //     await Shell.Current.DisplayAlert("Error", "Not enough mana to play the card", "OK");
        //     return;
        // }

        // Remove the card from the player's hand
        this._gameManager.CurrentPlayer.Hand.Remove(card);

        // Decrease the player's mana
        // this.AvailableMana -= this.SelectedHandCard.ManaCost;

        // Play the card
        this._gameManager.Board.PlayMinionFriendlySide(card);
        card.TriggerEffect(_gameManager.CurrentPlayer, _gameManager.EnemyPlayer, _gameManager.Board);

        // Notify the server
        await this._signalRService.SendPlayedCardToEnemyPlayer(card);
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
