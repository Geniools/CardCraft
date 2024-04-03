using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CardCraftClient.Model;
using CardCraftShared.Cards.Heroes;
using CardCraftShared.Cards.Minions;
using CardCraftShared.Core.Interfaces;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;

namespace CardCraftClient.ViewModel;

public partial class GamePageViewModel : BaseViewModel
{
    [ObservableProperty] 
    private int _timer;

    [ObservableProperty] 
    private int _availableMana;

    [ObservableProperty]
    private Player _currentPlayer;

    [ObservableProperty]
    private Player _enemyPlayer;

    [ObservableProperty]
    private ObservableCollection<IBaseCard> _currentPlayerHand;

    [ObservableProperty]
    private ObservableCollection<IBaseCard> _enemyPlayerHand;

    [ObservableProperty]
    private ObservableCollection<IBaseCard> _currentPlayerBoard;

    [ObservableProperty]
    private ObservableCollection<IBaseCard> _enemyPlayerBoard;

    public GamePageViewModel(GameManager gm)
    {
        this.Title = "Game";

        this.CurrentPlayer = gm.CurrentPlayer;
        this.EnemyPlayer = gm.EnemyPlayer;
        Trace.WriteLine(gm.CurrentPlayer.Hand.Cards);
        this.CurrentPlayerHand = new (gm.CurrentPlayer.Hand.Cards);

        gm.CurrentPlayer.Hand.Cards.ToObservableCollection().CollectionChanged += (sender, e) =>
        {
            this.CurrentPlayerHand = new (gm.CurrentPlayer.Hand.Cards);
        };

        this.Timer = gm.TurnTimer;
    }

    [RelayCommand]
    private async Task EndTurn()
    {

    }
}
