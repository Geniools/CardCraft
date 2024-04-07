using CardCraftClient.Model;
using CardCraftClient.Service;
using CardCraftClient.View;
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
    private readonly GameManager _gm;

    public StartPageViewModel(SignalRService signalRService, GameManager gm)
    {
        this._signalRService = signalRService;
        this._gm = gm;

        this._canStartGame = this._signalRService.HubConnection.State == HubConnectionState.Connected;
        this.IsBusy = false;
        this.GameName = "Card Craft";
        this.Title = "Join a Game:";

        this.CheckConnectionStatus();

        // must be set to a default value, otherwise the validation will fail
        this.LobbyCode = "773";
        this.Username = string.Empty;
    }

    private Task CheckConnectionStatus()
    { 
        return Task.Run(async () =>
        {
            while (true)
            {
                this.CanStartGame = this._signalRService.HubConnection.State == HubConnectionState.Connected;
                await Task.Delay(2000);
            }
        });
    }

    [RelayCommand]
    private async Task JoinGame()
    {
        this.IsBusy = true;
        bool isValid = !(string.IsNullOrWhiteSpace(this.Username) || string.IsNullOrWhiteSpace(this.LobbyCode));
        string errorMessage = "Please provide a valid Username and LobbyCode!\n\n";


        if (this.Username.Length is < 2 or > 12)
        {
            isValid = false;
            errorMessage += "Username must be between 2 and 12 characters!\n";
        }

        if (this.LobbyCode.Length is < 2 or > 6)
        {
            isValid = false;
            errorMessage += "Lobby code must be between 2 and 6 characters!\n";
        }

        // Check for the input to not contain any special characters or whitespaces
        if (this.Username.Any(char.IsWhiteSpace) || this.Username.Any(char.IsPunctuation))
        {
            isValid = false;
            errorMessage += "Username must not contain any special characters or whitespaces!\n";
        }

        // Check for the lobby code to only contain numbers or letters
        if (this.LobbyCode.Any(char.IsWhiteSpace) || this.LobbyCode.Any(char.IsPunctuation))
        {
            isValid = false;
            errorMessage += "Lobby code must only contain numbers or letters!\n";
        }

        // Check for the player to have selected a hero
        if (this._signalRService.Player.Hero is null)
        {
            isValid = false;
            errorMessage = "Select a hero first!\n";
        }

        if (!isValid)
        {
            this.IsBusy = false;
            await Shell.Current.DisplayAlert("Invalid input!", errorMessage, "Ok");
            return;
        }

        this._signalRService.Player.Name = this.Username;
        this._signalRService.LobbyCode = this.LobbyCode;

        // Before joining the game, clear the CurrentPlayer and EnemyPlayer in the GameManager
        this._gm.CurrentPlayer = null;
        this._gm.EnemyPlayer = null;

        // Join the Game
        await this._signalRService.JoinGame(this.LobbyCode, this.Username);

        this.IsBusy = false;
    }

    [RelayCommand]
    private async Task ChooseHero()
    {
        await Shell.Current.GoToAsync(nameof(HeroPage));
    }
}