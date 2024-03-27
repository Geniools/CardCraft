using CardCraftClient.Model;
using CardCraftClient.Service;
using CardCraftClient.View;
using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CardCraftClient.ViewModel;


public partial class LobbyPageViewModel : BaseViewModel
{
    private const int TIMER = 5;
    private GameManager _gm;

    [ObservableProperty]
    private Player _player1;
    
    [ObservableProperty]
    private Player _player2;

    [ObservableProperty]
    private string _lobbyCode;

    [ObservableProperty]
    private string _timerText;

    [ObservableProperty] 
    private bool _activityIndicatorRunning;

    public LobbyPageViewModel(SignalRService signalRService, GameManager gm)
    {
        this._gm = gm;
        this.TimerText = "Waiting for players...";
        this.LobbyCode = signalRService.LobbyCode;
        this.ActivityIndicatorRunning = true;

        signalRService.OnGameStartedEvent += async () =>
        {
            gm.IsGameStarted = true;
            await this.StartGame();
        };

        gm.CurrentPlayerChanged += (player) =>
        {
            this.Player1 = player;
        };

        gm.EnemyPlayerChanged += (player) =>
        {
            this.Player2 = player;
        };

        this.Player1 = gm.CurrentPlayer;
        this.Player2 = gm.EnemyPlayer;

        this.LobbyCode = signalRService.LobbyCode;
    }

    private async Task StartTimer()
    {
        this.ActivityIndicatorRunning = false;

        for (int i = TIMER; i > 0; i--)
        {
            this.TimerText = i.ToString();

            if (!CanStartGame())
            {
                this.TimerText = "Waiting for players...";
                this.ActivityIndicatorRunning = true;
            }

            await Task.Delay(1000);
        }
    }

    private async Task StartGame()
    {
        await this.StartTimer();

        if (!CanStartGame())
        {
            this.TimerText = "Waiting for players...";
            this.ActivityIndicatorRunning = true;
        }

        // If the contents of the "NavigateToGamePage" are placed here, the game will start regardless (could not fix it)
        await this._gm.NavigateToGamePage();
    }

    private bool CanStartGame()
    {
        return this.Player1 is not null && this.Player2 is not null;
    }
}

