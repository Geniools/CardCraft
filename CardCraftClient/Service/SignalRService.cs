using Microsoft.AspNetCore.SignalR.Client;

namespace CardCraftClient.Service;

public class SignalRService
{
    private readonly HubConnection _hubConnection;
    private int _reconnectAttempts = 0;
    private readonly int MAX_RECONNECT_ATTEMPTS = 3;

    public event Action OnConnectionError;

    public SignalRService()
    {
        this._hubConnection = new HubConnectionBuilder()
            .WithUrl("https://cardcraftserver.icysea-cde26e08.westeurope.azurecontainerapps.io/gameHub")
            .Build();

        this._hubConnection.Closed += async (error) =>
        {
            // If reconnect attempts exceed the maximum number of attempts, invoke the OnConnectionError event
            if (this._reconnectAttempts > this.MAX_RECONNECT_ATTEMPTS)
            {
                this.OnConnectionError?.Invoke();
                return;
            }

            // Wait for random time before trying to reconnect
            await Task.Delay(new Random().Next(0, 5) * 1000);
            this._reconnectAttempts++;
            // Try to reconnect
            await this.StartConnection();
        };
    }

    public Task StartConnection()
    {
        // return Task.Run(() =>
        // {
        //     Dispatcher.Dispatch(async () => await this._hubConnection.StartAsync());
        // });
        return this._hubConnection.StartAsync();
    }

    public Task StopConnection()
    {
        return this._hubConnection.StopAsync();
    }
}