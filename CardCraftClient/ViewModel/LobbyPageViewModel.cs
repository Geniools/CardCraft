using System.Diagnostics;
using CardCraftClient.Service;
using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CardCraftClient.ViewModel;


public partial class LobbyPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private Player? _player1;
    
    [ObservableProperty]
    private Player? _player2;

    [ObservableProperty]
    private string _lobbyCode;

    [ObservableProperty]
    private bool _canStartGame;

    public LobbyPageViewModel(SignalRService signalRService)
    {
        signalRService.OnGameJoinedEvent += async (player, otherPlayer) =>
        {
            Trace.WriteLine("==================================================================");
            Trace.WriteLine($"{player.Name} should be updated in the ViewModel rn");
            Trace.WriteLine("==================================================================");

            // The other player must be updated first
            // to avoid the player being overwritten
            if (otherPlayer is not null)
            {
                await this.JoinGame(otherPlayer);
            }

            await this.JoinGame(player);
        };

        signalRService.OnGameStartedEvent += async () =>
        {
            this.CanStartGame = true;
        };

        this.CanStartGame = false;
        this.LobbyCode = signalRService.LobbyCode;
    }

    private async Task JoinGame(Player player)
    {
        if (this.Player1 is null)
        {
            this.Player1 = player;
        }
        else
        {
            this.Player2 = player;
        }
    }
}

