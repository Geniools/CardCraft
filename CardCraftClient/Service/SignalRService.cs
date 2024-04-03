using System.Diagnostics;
using CardCraftClient.Core.Interfaces;
using CardCraftClient.View;
using CardCraftShared;
using CardCraftShared.Core.Other;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

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
            .WithUrl(CONNECTION_URL)
            .WithAutomaticReconnect()
            .ConfigureLogging(logging =>
            {
                logging.AddDebug();
                // This will set ALL logging to Debug level
                logging.SetMinimumLevel(LogLevel.Debug);
            })
            .Build();

        // Define connection events
        this._hubConnection.Closed += async (error) =>
        {
            // TODO: Inform users about the connection loss
        };

        this._hubConnection.Reconnecting += async (error) =>
        {
            // TODO: Inform users about the reconnection
        };

        // Define server callbacks

        // Called when an error occurs
        this._hubConnection.On<string>(ServerCallbacks.ErrorMessage, async (message) =>
        {
            Trace.WriteLine("\nError: ================== ");
            Trace.WriteLine(message);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Shell.Current.GoToAsync("..");

                Shell.Current.DisplayAlert("Error", message, "Ok");
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

        // Start the connection to the server
        this.StartConnection();
    }

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

    public Task StartConnection()
    {
        if (this._hubConnection.State == HubConnectionState.Disconnected)
        { 
            return Task.Run(async () =>
            {
                await this._hubConnection.StartAsync();

                this.Player.ConnectionId = this._hubConnection.ConnectionId;
            });
        }

        return Task.CompletedTask;
    }

    public async Task JoinGame(string lobbyCode, string playerName)
    {
        Player player = new Player();
        player.Name = playerName;

        player.PlayerSignalRDetails.HeroImage = this.Player.Hero.Image;
        player.PlayerSignalRDetails.HeroHealth = this.Player.Hero.Health;
        player.PlayerSignalRDetails.DeckCount = this.Player.Deck.Cards.Count;
        player.PlayerSignalRDetails.HandCount = this.Player.Hand.Cards.Count;

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

    public Task StopConnection()
    {
        return this._hubConnection.StopAsync();
    }
}