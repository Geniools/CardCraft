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
    private GameManager _gameManager;

    [ObservableProperty] private int _timer;

    // Current Player
    [ObservableProperty] private int _availableMana;
    [ObservableProperty] private Player _currentPlayer;
    [ObservableProperty] private int _currentPlayerHeroHealth;
    [ObservableProperty] private int _currentPlayerDeckCardCount;
    [ObservableProperty] private ObservableCollection<IBaseCard> _currentPlayerHand;
    [ObservableProperty] private IBaseCard _selectedHandCard;
    [ObservableProperty] private ObservableCollection<IBaseCard> _currentPlayerBoard;
    [ObservableProperty] private IBaseCard _selectedFriendlyBoardCard;

    // Enemy Player
    [ObservableProperty] private Player _enemyPlayer;
    [ObservableProperty] private int _enemyPlayerHeroHealth;
    [ObservableProperty] private int _enemyPlayerDeckCardCount;
    [ObservableProperty] private ObservableCollection<IBaseCard> _enemyPlayerHand;
    [ObservableProperty] private ObservableCollection<IBaseCard> _enemyPlayerBoard;
    [ObservableProperty] private IBaseCard _selectedEnemyBoardCard;

    public GamePageViewModel(GameManager gm)
    {
        this._gameManager = gm;
        this.Title = "Game";

        this.CurrentPlayer = gm.CurrentPlayer;
        this.EnemyPlayer = gm.EnemyPlayer;

        // Subscribe to game manager events
        gm.CurrentPlayer.Hand.Cards.CollectionChanged += (sender, e) =>
        {
            this.CurrentPlayerHand = gm.CurrentPlayer.Hand.Cards;
            this.CurrentPlayerDeckCardCount = gm.CurrentPlayer.Deck.Cards.Count;
        };

        gm.EnemyPlayer.Hand.Cards.CollectionChanged += (sender, e) =>
        {
            this.EnemyPlayerHand = gm.EnemyPlayer.Hand.Cards;
            this.EnemyPlayerDeckCardCount = gm.EnemyPlayer.Deck.Cards.Count;
        };

        gm.Board.FriendlySide.CollectionChanged += (sender, e) =>
        {
            this.CurrentPlayerBoard = gm.Board.FriendlySide;
        };

        gm.Board.EnemySide.CollectionChanged += (sender, e) =>
        {
            this.EnemyPlayerBoard = gm.Board.EnemySide;
        };

        // Set initial values
        this.CurrentPlayerHand = gm.CurrentPlayer.Hand.Cards;
        this.EnemyPlayerHand = gm.EnemyPlayer.Hand.Cards;

        this.CurrentPlayerBoard = gm.Board.FriendlySide;
        this.EnemyPlayerBoard = gm.Board.EnemySide;

        this.AvailableMana = gm.CurrentPlayer.Mana;

        this.CurrentPlayerHeroHealth = gm.CurrentPlayer.Hero.Health;
        // this.EnemyPlayerHeroHealth = gm.EnemyPlayer.Hero.Health;

        this.CurrentPlayerDeckCardCount = gm.CurrentPlayer.Deck.Cards.Count;
        // this.EnemyPlayerDeckCardCount = gm.EnemyPlayer.Deck.Cards.Count;

        this.Timer = gm.TurnTimer;
    }

    [RelayCommand]
    private async Task EndTurn()
    {

    }

    [RelayCommand]
    private async Task PlayCard()
    {
        if (this.SelectedHandCard is null)
        {
            await Shell.Current.DisplayAlert("Error", "No card to be played selected", "OK");
            return;
        }

        this._gameManager.Board.PlayMinionFriendlySide(this.SelectedHandCard);
    }

    [RelayCommand]
    private async Task AttackMinion()
    {

    }

    [RelayCommand]
    private async Task AttackHero()
    {

    }


    private void LogAvailableCollections()
    {
        Trace.WriteLine("Current Player Hand:");
        foreach (var card in this.CurrentPlayerHand)
        {
            Trace.WriteLine(card.Name);
        }
        //
        // Trace.WriteLine("Enemy Player Hand:");
        // foreach (var card in this.EnemyPlayerHand)
        // {
        //     Trace.WriteLine(card.Name);
        // }

        Trace.WriteLine("Current Player Board:");
        foreach (var card in this.CurrentPlayerBoard)
        {
            Trace.WriteLine(card.Name);
        }

        // Trace.WriteLine("Enemy Player Board:");
        // foreach (var card in this.EnemyPlayerBoard)
        // {
        //     Trace.WriteLine(card.Name);
        // }
    }
}
