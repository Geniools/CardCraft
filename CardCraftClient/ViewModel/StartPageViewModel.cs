using CardCraftClient.Service;
using CardCraftClient.View;
using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;

namespace CardCraftClient.ViewModel;

public partial class StartPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _username;

    [ObservableProperty]
    private string _lobbyCode;

    [ObservableProperty]
    private string _gameName;

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private bool _canStartGame;

    private readonly SignalRService _signalRService;

    public StartPageViewModel(SignalRService signalRService)
    {
        this._signalRService = signalRService;
        this._canStartGame = this._signalRService.HubConnection.State == HubConnectionState.Connected;
        this.IsBusy = false;
        this.GameName = "Card Craft";
        this.Title = "Join a Game:";

        this.CheckConnectionStatus();

        // Test credentials
        this.Username = "Chris";
        this.LobbyCode = "773";
    }

    private Task CheckConnectionStatus()
    { 
        return Task.Run(async () =>
        {
            while (true)
            {
                this.CanStartGame = this._signalRService.HubConnection.State == HubConnectionState.Connected;
                await Task.Delay(1000);
            }
        });
    }

    [RelayCommand]
    private async Task JoinGame()
    {
        this.IsBusy = true;

        this._signalRService.Player.Name = this.Username;
        this._signalRService.LobbyCode = this.LobbyCode;

        await this._signalRService.JoinGame(this.LobbyCode, this.Username);

        this.IsBusy = false;
    }
}