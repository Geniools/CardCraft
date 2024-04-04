using System.Collections.ObjectModel;
using System.Diagnostics;
using CardCraftClient.Core.Interfaces;
using CardCraftClient.Service;
using CardCraftClient.View;
using CardCraftShared;
using CardCraftShared.Core.Other;
using Microsoft.AspNetCore.SignalR.Client;

namespace CardCraftClient.Model;

public class GameManager : ISignalRObserver
{
    private object _lock = new();
    public const int DEFAULT_TURN_TIME = 30;

    private int _turnTimer;
    public int TurnTimer
    {
        get => this._turnTimer;
        set
        {
            if (value >= 0)
            {
                this._turnTimer = value;
                this.OnTurnTimerChanged?.Invoke(this._turnTimer);
            }
        }
    }
    public Action<int> OnTurnTimerChanged;

    private bool _isCurrentTurn;

    public bool IsCurrentTurn
    {
        get => this._isCurrentTurn;
        set
        {
            this._isCurrentTurn = value;
            
            this.OnCurrentTurnChanged?.Invoke(this._isCurrentTurn);
        }
    }
    public Action<bool> OnCurrentTurnChanged;

    private bool _isGameStarted;
    public bool IsGameStarted
    {
        get => this._isGameStarted;
        set
        {
            this._isGameStarted = value;

            if (this._isGameStarted)
            {
                this.OnGameStartedEvent?.Invoke();
            }
        }
    }

    public Action? OnGameStartedEvent;

    private readonly SignalRService _signalRService;

    // Players
    private Player? _currentPlayer;
    public Player? CurrentPlayer
    {
        get => this._currentPlayer;
        private set
        {
            this._currentPlayer = value;

            if (this._currentPlayer is not null)
            {
                this.CurrentPlayerChanged?.Invoke(this._currentPlayer);
            }
        }
    }
    public Action<Player?>? CurrentPlayerChanged;

    private Player? _enemyPlayer;
    public Player? EnemyPlayer
    {
        get => this._enemyPlayer;
        private set
        {
            this._enemyPlayer = value;

            if (this._enemyPlayer is not null)
            {
                this.EnemyPlayerChanged?.Invoke(this._enemyPlayer);
            }
        }
    }
    public Action<Player?>? EnemyPlayerChanged;

    // Game elements
    public Board Board { get; set; }
    public Graveyard Graveyard { get; set; }

    public GameManager(SignalRService signalRService)
    {
        this._signalRService = signalRService;
        // Register the observer
        this._signalRService.AddObserver(this);

        // Reset the game state
        this.Reset();
    }

    public async Task EndGame()
    {
        // Reset the players
        await this.Reset();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync($"///{nameof(StartPage)}");
        });

        // Will notify the server to remove the current player from the game online (so the player can connect to other games without issues)
        await this._signalRService.HubConnection.InvokeAsync(ServerCallbacks.LeaveGame);
    }

    public async Task AddPlayer(Player player)
    {
        Trace.WriteLine($"Player {player.Name} adding to the game! SignalR: {this._signalRService.Player.Name}");

        if (this._signalRService.Player.ConnectionId.Equals(player.ConnectionId))
        {
            this.CurrentPlayer ??= this._signalRService.Player;
        }
        else
        {
            this.EnemyPlayer = player;

            // Instantiate the enemy's hero from the received type
            this.EnemyPlayer.Hero = (BaseHero)Activator.CreateInstance(Type.GetType(player.PlayerSignalRDetails.HeroType) ?? throw new Exception("Hero type not found!"));
        }
    }

    public async Task NavigateToGamePage()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            if (this.CurrentPlayer is null || this.EnemyPlayer is null)
            {
                return;
            }

            // Check if the current page is not already the GamePage
            if (Shell.Current.CurrentPage is GamePage)
            {
                return;
            }

            if (!this.IsGameStarted)
            {
                return;
            }

            await Shell.Current.GoToAsync(nameof(GamePage));
        });
    }

    private Task Reset()
    {
        // Reset the game state
        this.IsGameStarted = false;
        // Reset the turn timer
        this.TurnTimer = 30;
        this.IsCurrentTurn = false;
        // Reset the players
        this.CurrentPlayer = null;
        this.EnemyPlayer = null;
        // Reset the board
        this.Board = new();
        // Reset the graveyard
        this.Graveyard = new();

        return Task.CompletedTask;
    }

    private async Task StartTurnTimer()
    {
        this.TurnTimer = DEFAULT_TURN_TIME;

        while (this.TurnTimer > 0)
        {
            await Task.Delay(1000);
            this.TurnTimer--;
        }
    }

    public async Task NextTurn()
    {
        // Draw a card
        this.CurrentPlayer?.DrawCard();

        // Increase the mana
        this.CurrentPlayer?.IncreaseMana();

        Trace.WriteLine($"Starting Turn Timer! PLAYER: {this._signalRService.Player.Name}");
        // Start the timer
        await this.StartTurnTimer();

        // After the timer ends, the turn will be ended
        await this.EndTurn();
    }

    public async Task OnTurnStarted(bool isFirstTurn)
    {
        Trace.WriteLine($"Is first turn: {isFirstTurn}. Player Start Turn: {this._signalRService.Player.Name}");
        this.IsCurrentTurn = true;

        if (!isFirstTurn)
        {
            await NextTurn();
        }
    }

    public async Task EndTurn()
    {
        this.IsCurrentTurn = false;

        // Reset the timer
        this.TurnTimer = DEFAULT_TURN_TIME;

        Trace.WriteLine($"Turn ended! PLAYER: {this._signalRService.Player.Name}");
        // Send the end turn signal to the server
        await this._signalRService.SendEndTurn();
    }

    public async Task OnEnemyPlayerUpdated(EnemyPlayerUpdateMessage message)
    {
        this.EnemyPlayer.Hero.Health = message.HeroHealth;

        // Add junk cards to the enemy player's hand to update the deck count
        DeckPool deckPool = new();
        for (int i = 0; i < message.PlayerDeckCardAmount; i++)
        {
            deckPool.AddCard(new JunkCard{Image = "deck.jpg", Name = "JunkCard" });
        }
        this.EnemyPlayer.Deck = deckPool;

        // Add junk cards to the enemy player's hand to update the hand count
        ObservableCollection<IBaseCard> updatedHandCards = new();
        for (int i = 0; i < message.PlayerHandCardAmount; i++)
        {
            updatedHandCards.Add(new JunkCard { Image = "deck.jpg", Name = "JunkCard"});
        }

        this.EnemyPlayer.Hand.Update(updatedHandCards);
    }

    public async Task OnCardPlayed(IBaseCard card)
    {
        this.Board.PlayMinionEnemySide(card);
    }

    public async Task OnGameJoined(Player player, Player? otherPlayer)
    {
        // The other player must be updated first
        // to avoid the player being overwritten
        if (otherPlayer is not null)
        {
            await this.AddPlayer(otherPlayer);
        }

        await this.AddPlayer(player);
    }

    public async Task OnGameStarted()
    {
        this.IsGameStarted = true;

        // Check if the current player is instantiated, otherwise, try waiting for 3 times, then end the game
        await this.CheckCurrentPlayer();

        if (this.CurrentPlayer is null)
        {
            await this.EndGame();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                this.IsGameStarted = false;

                this.CurrentPlayer = null;
                this.EnemyPlayer = null;
                await Shell.Current.DisplayAlert("Error", "Could not start the game! Try again!", "Ok");
            });
            return;
        }

        this.CurrentPlayer.Deck.Shuffle();

        // Draw the initial cards
        for (int i = 0; i < 3; i++)
        {
            this.CurrentPlayer.DrawCard();
        }

        // Inform the server to choose a player to start the turn
        await this._signalRService.SendPickRandomPlayerToStartTurn();
    }

    private async Task CheckCurrentPlayer()
    {
        for (int i = 0; i < 3; i++)
        {
            if (this.CurrentPlayer is not null)
            {
                break;
            }

            await Task.Delay(1000);
        }
    }

    public void OnCardUnalive(object sender, EventArgs e)
    {
        if (sender is IBaseCard card)
        {
            this.Graveyard.AddCard(card);
        }
    }

    public async Task OnGameEnded()
    {
        await this.EndGame();
    }
}
