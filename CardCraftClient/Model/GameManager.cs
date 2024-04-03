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
    public const int TURN_TIME = 30;
    public int TurnTimer { get; set; }

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
    private Board Board { get; set; }
    private Graveyard Graveyard { get; set; }

    public GameManager(SignalRService signalRService)
    {
        this._signalRService = signalRService;
        // Register the observer
        this._signalRService.AddObserver(this);

        // Reset the game state
        this.Reset();
    }

    public async Task NextTurn()
    {
        // Start the timer
        this.StartTurnTimer();

        // Draw a card
        this.CurrentPlayer?.DrawCard();

        // Increase the mana
        this.CurrentPlayer?.IncreaseMana();
    }

    private async Task StartTurnTimer()
    {
        this.TurnTimer = TURN_TIME;

        while (this.TurnTimer > 0)
        {
            await Task.Delay(1000);
            this.TurnTimer--;
        }

        // End the turn
        await this.EndTurn();
    }

    private async Task EndTurn()
    {
        // TODO: Disable the buttons and selections
        // TODO: Send the end turn signal to the server
    }

    public async Task EndGame()
    {
        // Reset the players
        await this.Reset();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            Shell.Current.GoToAsync($"///{nameof(StartPage)}");
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
        }
    }

    public async Task NavigateToGamePage()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            if (this.CurrentPlayer is not null && this.EnemyPlayer is not null)
            {
                Trace.WriteLine(nameof(Shell.Current.CurrentPage));

                // Check if the current page is not already the GamePage
                if (Shell.Current.CurrentPage is GamePage)
                {
                    return;
                }

                await Shell.Current.GoToAsync(nameof(GamePage));
            }
        });
    }

    private Task Reset()
    {
        // Reset the game state
        this.IsGameStarted = false;
        // Reset the players
        this.CurrentPlayer = null;
        this.EnemyPlayer = null;
        // Reset the board
        this.Board = new();
        // Reset the graveyard
        this.Graveyard = new();

        return Task.CompletedTask;
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
        for (int i = 0; i < 3; i++)
        {
            if (this.CurrentPlayer is not null)
            {
                break;
            }

            await Task.Delay(1000);
        }

        if (this.CurrentPlayer is null)
        {
            await this.EndGame();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                this.IsGameStarted = false;
                await Shell.Current.DisplayAlert("Error", "Could not start the game! Try again!", "Ok");
            });
            return;
        }

        this.CurrentPlayer.Deck.Shuffle();

        // Draw the initial cards
        for (int i = 0; i < 3; i++)
        {
            this.CurrentPlayer.Deck.DrawCard();
        }

        // TODO: Inform the server to choose a player to start the turn
    }

    public async Task OnGameEnded()
    {
        await this.EndGame();
    }
}
