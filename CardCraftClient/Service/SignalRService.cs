using System.Diagnostics;
using CardCraftClient.Core.Interfaces;
using CardCraftClient.View;
using CardCraftShared;
using CardCraftShared.Core.Other;
using Microsoft.AspNetCore.SignalR.Client;

namespace CardCraftClient.Service;

public class SignalRService
{
    private IList<ISignalRObserver> _observers = new List<ISignalRObserver>();

    private const string CONNECTION_URL = "https://cardcraftserver.azurewebsites.net/gameHub";
    // Used for local testing (debugging) - requires the server to be running locally
    private const string CONNECTION_URL_LOCAL = "http://localhost:5228/gameHub";

    public Player Player { get; set; }
    public string LobbyCode { get; set; }

    private readonly HubConnection _hubConnection;
    public HubConnection HubConnection => this._hubConnection;

    public SignalRService()
    {
        this.Player = new();

        this._hubConnection = new HubConnectionBuilder()
            .WithUrl(CONNECTION_URL) // Change to CONNECTION_URL for the online server
            .WithAutomaticReconnect()
            // .ConfigureLogging(logging =>
            // {
            //     logging.AddDebug();
            //     // This will set ALL logging to Debug level
            //     logging.SetMinimumLevel(LogLevel.Debug);
            // })
            .Build();

        // Define connection events
        this._hubConnection.Closed += async (error) =>
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                Shell.Current.DisplayAlert("Error", "Connection with the server is lost! \n Try to run the application again.", "Ok");

                await Shell.Current.GoToAsync($"///{nameof(StartPage)}");
            });
        };

        this._hubConnection.Reconnecting += async (error) =>
        {
            // TODO: Inform users about the reconnection
        };

        // Define server callbacks

        // Called when an error occurs
        this._hubConnection.On<string>(ServerCallbacks.ErrorMessage, async (message) =>
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                Shell.Current.DisplayAlert("Error", message, "Ok");

                await Shell.Current.GoToAsync($"///{nameof(StartPage)}");
            });
        });

        // Called when a player joins the game
        this._hubConnection.On<string, Player, Player?>(ServerCallbacks.GameJoined, async (lobbyCode, player, otherPlayer) =>
        {

            await this.NotifyObserversGameJoined(player, otherPlayer);
        });

        // Called when the game is started
        this._hubConnection.On<string>(ServerCallbacks.GameStarted, async (lobbyCode) =>
        {
            await this.NotifyObserversGameStarted();
        });

        // Called when the game is ended
        this._hubConnection.On(ServerCallbacks.GameEnded, async () =>
        {
            await this.NotifyObserversGameEnded();
        });

        // Called when a card is played
        this._hubConnection.On<CardMessage>(ServerCallbacks.CardPlayed, async (cardMessage) =>
        {
            IBaseCard card;

            // Create a card from the message
            Type? cardType = Type.GetType(cardMessage.CardTypeName);

            if (cardType is null)
            {
                throw new Exception("Card type not found");
            }

            // Create an instance of the card
            card = (IBaseCard)Activator.CreateInstance(cardType);

            card.ManaCost = cardMessage.ManaCost;
            
            // If the card is a minion, set the attack and health
            if (card is BaseMinion minionCard)
            {
                minionCard.Attack = cardMessage.Attack;
                minionCard.Health = cardMessage.Health;
                minionCard.ManaCost = cardMessage.ManaCost;
                minionCard.Id = cardMessage.Id;
            }

            Trace.WriteLine($"Card received: {cardMessage.Id}");

            await this.NotifyObserversCardPlayed(card);
        });

        // Called when the enemy player is updated
        this._hubConnection.On<EnemyPlayerCardAmountUpdateMessage>(ServerCallbacks.EnemyPlayerCardAmountUpdated, async (message) =>
        {
            await this.NotifyObserversEnemyPlayerCardAmountUpdated(message);
        });

        // Called when a minion is updated
        this._hubConnection.On<MinionCardUpdatedMessage>(ServerCallbacks.MinionUpdated, async (message) =>
        {
            await this.NotifyObserversMinionUpdated(message);
        });

        // Called when the enemy player hero is updated
        this._hubConnection.On<EnemyPlayerHeroUpdateMessage>(ServerCallbacks.EnemyPlayerHeroUpdated, async (message) =>
        {
            await this.NotifyObserversEnemyPlayerHeroUpdated(message);
        });

        // Called when the turn starts
        this._hubConnection.On<bool>(ServerCallbacks.StartTurn, async (isFirstTurn) =>
        {
            await this.NotifyObserversTurnStarted(isFirstTurn);
        });

        // Start the connection to the server
        _ = this.StartConnection();
    }

    public async Task StartConnection()
    {
        if (this._hubConnection.State == HubConnectionState.Disconnected)
        {
            await this._hubConnection.StartAsync();

            this.Player.ConnectionId = this._hubConnection.ConnectionId;
        }
    }

    public async Task StopConnection()
    {
        await this._hubConnection.StopAsync();
    }

    // Observer pattern
    public void AddObserver(ISignalRObserver observer)
    {
        this._observers.Add(observer);
    }

    public void RemoveObserver(ISignalRObserver observer)
    {
        this._observers.Remove(observer);
    }

    private async Task NotifyObserversGameJoined(Player player, Player? otherPlayer)
    {
        foreach (ISignalRObserver observer in this._observers)
        {
            await observer.OnGameJoined(player, otherPlayer);
        }
    }

    private async Task NotifyObserversGameStarted()
    {
        foreach (ISignalRObserver observer in this._observers)
        {
            await observer.OnGameStarted();
        }
    }

    private async Task NotifyObserversGameEnded()
    {
        foreach (ISignalRObserver observer in this._observers)
        {
            await observer.OnGameEnded();
        }
    }

    private async Task NotifyObserversCardPlayed(IBaseCard card)
    {
        foreach (ISignalRObserver observer in this._observers)
        {
            await observer.OnCardPlayed(card);
        }
    }

    private async Task NotifyObserversEnemyPlayerCardAmountUpdated(EnemyPlayerCardAmountUpdateMessage message)
    {
        foreach (ISignalRObserver observer in this._observers)
        {
            await observer.OnEnemyPlayerCardAmountUpdated(message);
        }
    }

    private async Task NotifyObserversEnemyPlayerHeroUpdated(EnemyPlayerHeroUpdateMessage message)
    {
        foreach (ISignalRObserver observer in this._observers)
        {
            await observer.OnEnemyPlayerHeroUpdated(message);
        }
    }

    private async Task NotifyObserversTurnStarted(bool isFirstTurn = false)
    {
        foreach (ISignalRObserver observer in this._observers)
        {
            await observer.OnTurnStarted(isFirstTurn);
        }
    }

    private async Task NotifyObserversMinionUpdated(MinionCardUpdatedMessage message)
    {
        foreach (ISignalRObserver observer in this._observers)
        {
            await observer.OnMinionUpdated(message);
        }
    }

    // ==================

    // Functions to send messages to the server
    public async Task JoinGame(string lobbyCode, string playerName)
    {
        Player player = new Player();
        player.Name = playerName;

        player.PlayerSignalRDetails.HeroType = this.Player.Hero.GetType().FullName;

        try
        {
            // The navigation must happen before the HubConnection call
            // Otherwise, the LobbyPageViewModel will not be able to handle the event
            await Shell.Current.GoToAsync(nameof(LobbyPage));
            await this.HubConnection.InvokeAsync(ServerCallbacks.JoinGame, player, lobbyCode);
        }
        catch (Exception e)
        {
            Trace.WriteLine(e);

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                Shell.Current.GoToAsync("..");
                await Shell.Current.DisplayAlert("Error", e.Message, "Ok");
            });
        }
    }

    public async Task SendPlayedCardToEnemyPlayer(IBaseCard card)
    {
        // Make a message from the card
        CardMessage cardMessage = new CardMessage
        {
            CardTypeName = card.GetType().FullName,
            ManaCost = card.ManaCost,
        };

        if (card is BaseMinion minionCard)
        {
            cardMessage.Attack = minionCard.Attack;
            cardMessage.Health = minionCard.Health;
            cardMessage.Id = minionCard.Id;
        }

        await this.HubConnection.InvokeAsync(ServerCallbacks.PlayCard, cardMessage);
    }

    public async Task SendCardAmountUpdateEnemyPlayer(EnemyPlayerCardAmountUpdateMessage message)
    {
        await this.HubConnection.InvokeAsync(ServerCallbacks.UpdateCardAmountEnemyPlayer, message);
    }

    public async Task SendHeroUpdateEnemyPlayer(EnemyPlayerHeroUpdateMessage message)
    {
        await this.HubConnection.InvokeAsync(ServerCallbacks.UpdateEnemyPlayerHero, message);
    }

    public async Task SendPickRandomPlayerToStartTurn()
    {
        await this.HubConnection.InvokeAsync(ServerCallbacks.PickRandomPlayerToStartTurn);
    }

    public async Task SendMinionUpdated(MinionCardUpdatedMessage message)
    {
        await this.HubConnection.InvokeAsync(ServerCallbacks.UpdateMinion, message);
    }

    public async Task SendEndTurn()
    {
        await this.HubConnection.InvokeAsync(ServerCallbacks.EndTurn);
    }

    public void Reset()
    {
        this.Player.Hero.Health = BaseHero.DefaultHealth;
        this.Player.Mana = 1;

        this.Player.Hand.Clear();
        this.Player.Deck.Reset();
    }
}