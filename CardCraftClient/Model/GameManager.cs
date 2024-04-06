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
    public const int DEFAULT_TURN_TIME = 60;
    public const int MAX_MANA = 10;

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
    private Player _currentPlayer;
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

    private Player _enemyPlayer;
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
    public Action<IBaseCard>? OnTemporaryCardDisplayAction;
    public Graveyard Graveyard { get; set; }
    public int AvailableMana { get; set; }

    public GameManager(SignalRService signalRService)
    {
        this._signalRService = signalRService;
        // Register the observer
        this._signalRService.AddObserver(this);

        // Set the initial values
        this.IsGameStarted = false;

        // Turn timer
        this.TurnTimer = DEFAULT_TURN_TIME;
        this.IsCurrentTurn = false;

        this.Board = new();
        this.Graveyard = new();

        // Subscribe to events
        this.Board.OnMinionDeath += this.OnCardUnalive;
    }

    public async Task EndGame()
    {
        // Reset the players
        this.Reset();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync($"///{nameof(StartPage)}");
        });

        // Will notify the server to remove the current player from the game online (so the player can connect to other games without issues)
        await this._signalRService.HubConnection.InvokeAsync(ServerCallbacks.LeaveGame);
    }

    public async Task AddPlayer(Player player)
    {
        // Trace.WriteLine($"Player {player.Name} adding to the game! SignalR: {this._signalRService.Player.Name}");

        if (this._signalRService.Player.ConnectionId.Equals(player.ConnectionId))
        {
            this.CurrentPlayer = this._signalRService.Player;

            // Subscribe to the Hero's death event
            this.CurrentPlayer.Hero.OnDeath += async (hero) =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    Shell.Current.DisplayAlert("Game Over", "You lost the game!", "Ok");
                });

                await this.EndGame();
            };
        }
        else
        {
            this.EnemyPlayer = player;

            // Instantiate the enemy's hero from the received type
            this.EnemyPlayer.Hero = (BaseHero)Activator.CreateInstance(Type.GetType(player.PlayerSignalRDetails.HeroType) ?? throw new Exception("Hero type not found!"));

            // Subscribe to the Hero's death event
            this.EnemyPlayer!.Hero.OnDeath += async (hero) =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    Shell.Current.DisplayAlert("Game Over", "You won the game!", "Ok");
                });

                await this.EndGame();
            };
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

    private void Reset()
    {
        // Reset the game state
        this.IsGameStarted = false;
        
        // Reset the turn timer
        this.TurnTimer = DEFAULT_TURN_TIME;
        this.IsCurrentTurn = false;

        // Reset the players
        this.CurrentPlayer = null;
        this.EnemyPlayer = null;

        this._signalRService.Reset();
        
        // Reset the board
        this.Board = new();
        
        // Reset the graveyard
        this.Graveyard = new();
    }

    private async Task StartTurnTimer()
    {
        this.TurnTimer = DEFAULT_TURN_TIME;

        while (this.TurnTimer > 0)
        {
            await Task.Delay(1000);
            this.TurnTimer--;

            if (!this.IsCurrentTurn)
            {
                this.TurnTimer = 0;
                break;
            }
        }

        this.IsCurrentTurn = false;
    }

    public async Task NextTurn()
    {
        // Draw a card
        this.CurrentPlayer?.DrawCard();

        // Increase the mana
        if (AvailableMana < MAX_MANA)
        {
            AvailableMana++;
        }
        this.CurrentPlayer.Mana = AvailableMana;

        // Make every minion from the board be able to attack
        this.Board.EnableMinionsToAttack();

        // Start the timer
        await this.StartTurnTimer();

        // After the timer ends, the turn will be ended
        await this.EndTurn();
    }

    public async Task OnTurnStarted(bool isFirstTurn)
    {
        this.IsCurrentTurn = true;

        if (!isFirstTurn)
        {
            await NextTurn();
        }
    }

    public async Task EndTurn()
    {
        this.IsCurrentTurn = false;

        // Send the end turn signal to the server
        await this._signalRService.SendEndTurn();
    }

    public async Task OnEnemyPlayerCardAmountUpdated(EnemyPlayerCardAmountUpdateMessage message)
    {
        // Add junk cards to the enemy player's hand to update the deck count
        List<IBaseCard> newDeck = new();
        for (int i = 0; i < message.PlayerDeckCardAmount; i++)
        {
            newDeck.Add(new JunkCard{Image = "deck.jpg", Name = "JunkCard" });
        }
        this.EnemyPlayer.Deck.Update(newDeck);

        // Add junk cards to the enemy player's hand to update the hand count
        ObservableCollection<IBaseCard> updatedHandCards = new();
        for (int i = 0; i < message.PlayerHandCardAmount; i++)
        {
            updatedHandCards.Add(new JunkCard { Image = "deck.jpg", Name = "JunkCard"});
        }

        this.EnemyPlayer.Hand.Update(updatedHandCards);
    }

    public async Task OnEnemyPlayerHeroUpdated(EnemyPlayerHeroUpdateMessage message)
    {
        this.CurrentPlayer.Hero.Health = message.SenderEnemyHeroHealth;
        this.EnemyPlayer.Hero.Health = message.SenderFriendlyHeroHealth;
    }

    public async Task OnCardPlayed(IBaseCard card)
    {
        // If the card is a minion, play it on the board
        if (card is BaseMinion minion)
        {
            this.Board.PlayMinionEnemySide(minion);
        }

        // Display the card as a temporary card on the screen
        this.OnTemporaryCardDisplayAction?.Invoke(card);
    }

    public async Task OnMinionUpdated(MinionCardUpdatedMessage message)
    {
        BaseMinion? minion = null;

        // Find the minion on the board based on the Id
        if (message.SenderBoardSide.Equals("enemy"))
        {
            minion = this.Board.FriendlySide.FirstOrDefault(m => m.Id.Equals(message.Id));
        }
        else
        {
            minion = this.Board.EnemySide.FirstOrDefault(m => m.Id.Equals(message.Id));
        }

        if (minion is null)
        {
            return;
        }

        // Update the minion's properties only if the values are different
        if (minion.Health != message.Health)
        {
            minion.Health = message.Health;
        }

        if (minion.Attack != message.Attack)
        {
            minion.Attack = message.Attack;
        }

        if (minion.Name != message.Name)
        {
            minion.Name = message.Name;
        }

        if (minion.Description != message.Description)
        {
            minion.Description = message.Description;
        }

        if (minion.Image != message.Image)
        {
            minion.Image = message.Image;
        }
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

    public void OnCardUnalive(IBaseCard card)
    {
        this.Graveyard.AddCard(card);
    }

    public async Task OnGameEnded()
    {
        await this.EndGame();
    }
}
