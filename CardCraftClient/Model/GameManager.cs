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
    public bool IsGameStarted { get; set; }
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
        // TODO: Implement the next turn logic

        // Start the timer
        this.StartTurnTimer();
    }

    private async Task StartTurnTimer()
    {
        
    }

    public async Task EndGame()
    {
        // Reset the players
        this.Reset();

        Shell.Current.GoToAsync("///" + nameof(StartPage));
        // Will notify the server to remove the current player from the game online (so the player can connect to other games without issues)
        await this._signalRService.HubConnection.InvokeAsync(ServerCallbacks.LeaveGame);
    }

    public async Task AddPlayer(Player player)
    {
        if (this._signalRService.Player.ConnectionId.Equals(player.ConnectionId))
        {
            this.CurrentPlayer = this._signalRService.Player;
        }
        else
        {
            this.EnemyPlayer = player;
        }
    }

    public async Task RemovePlayer(Player? player)
    {
        if (player is null)
        {
            return;
        }

        if (this.CurrentPlayer?.ConnectionId == player.ConnectionId)
        {
            this.CurrentPlayer = null;
        }
        else if (this.EnemyPlayer?.ConnectionId == player.ConnectionId)
        {
            this.EnemyPlayer = null;
        }

        // If one of the players leaves the game, end the game
        if (this.CurrentPlayer is null || this.EnemyPlayer is null)
        {
            if (IsGameStarted)
            {
                await this.EndGame();
            }
        }
    }

    public async Task NavigateToGamePage()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            if (this.CurrentPlayer is not null && this.EnemyPlayer is not null)
            {
                Shell.Current.GoToAsync(nameof(GamePage));
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

    public async Task OnGameJoined(Player player, Player otherPlayer)
    {
        // The other player must be updated first
        // to avoid the player being overwritten
        if (otherPlayer is not null)
        {
            await this.AddPlayer(otherPlayer);
        }

        await this.AddPlayer(player);
    }

    public async Task OnGameLeft(Player player)
    {
        await this.RemovePlayer(player);
    }

    public async Task OnGameStarted()
    {
        this.IsGameStarted = true;
        this.OnGameStartedEvent?.Invoke();

        this.CurrentPlayer.Deck.Shuffle();
    }

    public async Task OnGameEnded()
    {
        await this.EndGame();
    }
}
