using System.Diagnostics;
using CardCraftShared;
using CardCraftShared.Core.Other;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace CardCraftClient.Service;

public class SignalRService
{
    private const string CONNECTION_URL = "https://cardcraftserver.azurewebsites.net/gameHub";
    // Used for local testing (debugging) - requires the server to be running locally
    private const string CONNECTION_URL_LOCAL = "http://localhost:5228/gameHub";

    public Player Player { get; set; }
    public string LobbyCode { get; set; }

    private readonly HubConnection _hubConnection;
    public HubConnection HubConnection => this._hubConnection;

    public event Action? OnConnectionError;
    public event Action? OnGameStartedEvent;
    public event Action<Player, Player?>? OnGameJoinedEvent;
    public event Action<Player?>? OnGameLeftEvent;

    public SignalRService()
    {
        this.Player = new();

        this._hubConnection = new HubConnectionBuilder()
            .WithUrl(CONNECTION_URL_LOCAL)
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
        this._hubConnection.On<string, Player, Player?>(ServerCallbacks.GameJoined, (lobbyCode, player, otherPlayer) =>
        {
            Trace.WriteLine("==================================================================");
            Trace.WriteLine($"Player {player.Name} joined the game with lobby code {lobbyCode}!");
            Trace.WriteLine("==================================================================");

            this.OnGameJoinedEvent?.Invoke(player, otherPlayer);
        });

        // Called when a player leaves the game
        this._hubConnection.On<Player?>(ServerCallbacks.GameLeft, (player) =>
        {
            this.OnGameLeftEvent?.Invoke(player);
        });

        // Called when the game is started
        this._hubConnection.On<string>(ServerCallbacks.GameStarted, (lobbyCode) =>
        {
            this.OnGameStartedEvent?.Invoke();
        });

        // Start the connection to the server
        this.StartConnection();
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

        try
        {
            // The navigation must happen before the HubConnection call
            // Otherwise, the LobbyPageViewModel will not be able to handle the event
            // await Shell.Current.GoToAsync(nameof(LobbyPage));
            await Shell.Current.GoToAsync("LobbyPage");
            await this.HubConnection.InvokeAsync(ServerCallbacks.JoinGame, player, lobbyCode);
        }
        catch (Exception e)
        {
            Trace.WriteLine(e);

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.DisplayAlert("Error", e.Message, "Ok");
            });
        }
    }

    public Task StopConnection()
    {
        return this._hubConnection.StopAsync();
    }
}