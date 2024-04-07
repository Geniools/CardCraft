using CardCraftClient.Model;
using CardCraftClient.Service;
using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CardCraftClient.ViewModel;


public partial class LobbyPageViewModel : BaseViewModel
{
    private const int TIMER_DEFAULT = 5;
    public Action OnTimerCompletedAction;
    private GameManager _gm;

    [ObservableProperty]
    private Player _player1;
    
    [ObservableProperty]
    private Player _player2;

    [ObservableProperty]
    private string _lobbyCode;

    [ObservableProperty]
    private string _timerText;

    public LobbyPageViewModel(SignalRService signalRService, GameManager gm)
    {
        signalRService.Reset();

        this._gm = gm;
        this.TimerText = "Waiting for players";
        this.LobbyCode = signalRService.LobbyCode;

        this._gm.OnGameStartedEvent += async () =>
        {
            await this.StartTimer();
        };

        this.OnTimerCompletedAction = async () =>
        {
            await this._gm.NavigateToGamePage();
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

        this.PlayWaitingPlayersAnimation();
    }

    private async Task StartTimer()
    {
        for (int i = TIMER_DEFAULT; i >= 0; i--)
        {
            this.TimerText = i.ToString();

            if (!CanStartGame())
            {
                this.TimerText = "Waiting for players";
            }

            // If the timer is complete, start the game
            if (i == 0)
            {
                this.TimerText = "Game starting";
                this.OnTimerCompletedAction?.Invoke();
            }

            await Task.Delay(1000);
        }
    }

    private async Task PlayWaitingPlayersAnimation()
    {
        while (true)
        {
            if (CanStartGame())
            {
                break;
            }

            if (this.TimerText.Substring(this.TimerText.Length - 3).Equals("..."))
            {
                this.TimerText = "Waiting for players";
                await Task.Delay(1000);
            }

            this.TimerText += ".";
            await Task.Delay(1000);
        }
    }

    private bool CanStartGame()
    {
        return this.Player1 is not null && this.Player2 is not null;
    }

    [RelayCommand]
    private async Task ReturnToStartPage()
    {
        this._gm.EndGame();
    }
}

