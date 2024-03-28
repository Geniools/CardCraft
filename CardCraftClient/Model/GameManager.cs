using System.Diagnostics;
using CardCraftClient.Service;
using CardCraftClient.View;
using CardCraftShared;
using CardCraftShared.Cards.Heroes;
using CardCraftShared.Cards.Minions;
using CardCraftShared.Cards.Spells;
using CardCraftShared.Core.Decorators;
using CardCraftShared.Core.Interfaces;
using CardCraftShared.Core.Other;
using Microsoft.AspNetCore.SignalR.Client;

namespace CardCraftClient.Model;

public class GameManager
{
    public bool IsGameStarted { get; set; }
    private readonly SignalRService _signalRService;

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

    private Board Board { get; set; }
    private Graveyard Graveyard { get; set; }

    public GameManager(SignalRService signalRService)
    {
        this._signalRService = signalRService;
        this.Reset();

        signalRService.OnGameJoinedEvent += async (player, otherPlayer) =>
        {
            Trace.WriteLine("==================================================================");
            Trace.WriteLine($"PLAYER: {player.Name} should be updated in the ViewModel rn");
            Trace.WriteLine("==================================================================");

            Trace.WriteLine("==================================================================");
            Trace.WriteLine($"OTHER PLAYER: {otherPlayer?.Name} should be updated in the ViewModel rn");
            Trace.WriteLine("==================================================================");

            // The other player must be updated first
            // to avoid the player being overwritten
            if (otherPlayer is not null)
            {
                await this.AddPlayer(otherPlayer);
            }

            await this.AddPlayer(player);
        };

        signalRService.OnGameLeftEvent += async (player) =>
        {
            await this.RemovePlayer(player);
        };
    }

    public async Task StartGame()
    {
        // await this._signalRService.StartConnection();

        TestStuff();
    }

    public async Task EndGame()
    {
        Trace.WriteLine("GAME ENDED!!!");
        
        // Reset the players
        this.Reset();

        Shell.Current.GoToAsync("///" + nameof(StartPage));
        await this._signalRService.HubConnection.InvokeAsync(ServerCallbacks.LeaveGame);
    }

    public async Task AddPlayer(Player player)
    {
        if (this._signalRService.Player.ConnectionId.Equals(player.ConnectionId))
        {
            this.CurrentPlayer = player;
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

    public void TestStuff()
    {
        DeckPool deck = new();
        BaseHero hero = new AlexHero(1, "", "", "aboba");
        Player player = new();
        player.Hero = hero;
        player.Deck = deck;
        player.Hand = new Hand();
        player.Name = "Player";
        
        Board board = new();

        IMinion minion = new AlexCard();
        IMinion minion2 = new AlexCard();
        ResitSpell spell = new();

        deck.AddCard(minion);
        deck.AddCard(minion2);
        deck.AddCard(spell);
        deck.Shuffle();

        Player player1 = new();
        Player player2 = new();
        AddPlayer(player1);
        AddPlayer(player2);

        player1.DrawCard();
        player2.DrawCard();

        player1.PlayCard(minion, board);
        player2.PlayCard(minion, board);

        Trace.WriteLine(board.EnemySide.Count);
        Trace.WriteLine(board.FriendlySide.Count);

        Trace.WriteLine(deck);
        minion = new DivineShield(minion);
        minion2 = new TauntDecorator(minion2);
        Trace.WriteLine(minion.Health);
        minion2.AttackMinion(minion);
        Trace.WriteLine(minion.Health);
        minion2.AttackMinion(minion);
        Trace.WriteLine(minion.Health);
    }
}
