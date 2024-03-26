using System.Diagnostics;
using CardCraftClient.View;
using CardCraftShared;
using CardCraftShared.Cards.Heroes;
using CardCraftShared.Core.Other;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace CardCraftClient.Service;

public class SignalRService
{
    public Player? Player { get; set; }
    public string LobbyCode { get; set; }
    private readonly HubConnection _hubConnection;
    public HubConnection HubConnection => this._hubConnection;
    private int _reconnectAttempts = 0;
    private const int MAX_RECONNECT_ATTEMPTS = 3;
    private const string CONNECTION_URL = "https://cardcraftserver.azurewebsites.net/gameHub";
    private const string CONNECTION_URL_LOCAL = "http://localhost:5228/gameHub";

    public event Action? OnConnectionError;
    public event Action? OnGameStartedEvent;
    public event Action<Player>? OnGameJoinedEvent;

    public SignalRService()
    {
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
        this._hubConnection.On<string>(ServerCallbacks.ErrorMessage, async (message) =>
        {
            Shell.Current.DisplayAlert("Error", message, "Ok");

            await Shell.Current.GoToAsync("..");
        });

        this._hubConnection.On<string, Player>(ServerCallbacks.GameJoined, (lobbyCode, player) =>
        {
            Trace.WriteLine("==================================================================");
            Trace.WriteLine($"Player {player.Name} joined the game with lobby code {lobbyCode}!");
            Trace.WriteLine("==================================================================");
            this.OnGameJoinedEvent?.Invoke(player);
        });

        this._hubConnection.On<string>(ServerCallbacks.GameStarted, (lobbyCode) =>
        {
            this.OnGameStartedEvent?.Invoke();
        });

        this.StartConnection();
    }

    public Task StartConnection()
    {
        if (this._hubConnection.State == HubConnectionState.Disconnected)
        { 
            return Task.Run(async () =>
            {
                await this._hubConnection.StartAsync();
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
            await Shell.Current.GoToAsync(nameof(LobbyPage));
            await this.HubConnection.InvokeAsync(ServerCallbacks.JoinGame, player, lobbyCode);
        }
        catch (Exception e)
        {
            Trace.WriteLine(e);
            await Shell.Current.DisplayAlert("Error", e.Message, "Ok");
        }
    }

    public Task StopConnection()
    {
        return this._hubConnection.StopAsync();
    }
}