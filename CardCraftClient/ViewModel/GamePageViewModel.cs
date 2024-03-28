using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CardCraftClient.Model;
using CardCraftShared.Cards.Heroes;
using CardCraftShared.Cards.Minions;
using CardCraftShared.Core.Interfaces;

namespace CardCraftClient.ViewModel;

public partial class GamePageViewModel : BaseViewModel
{
    [ObservableProperty]
    private Player _currentPlayer;

    [ObservableProperty]
    private Player _enemyPlayer;
    
    [ObservableProperty]
    private int _manaCost;

    [ObservableProperty]
    private int _attack;

    [ObservableProperty]
    private int _health;

    [ObservableProperty] 
    private string _description;

    [ObservableProperty] private int _id;

    public ObservableCollection<IBaseCard> PlayerHand { get; set; }

    public GamePageViewModel(GameManager gm)
    {
        this.Title = "Game";
        this.CurrentPlayer = gm.CurrentPlayer;
        this.EnemyPlayer = gm.EnemyPlayer;
        this.Attack = 5;
        this.Health = 10;
        this.ManaCost = 3;
        this.Description = "nice";
        this.Id = 1;
        DeckPool deck = new();
        BaseHero hero = new AlexHero();
        Player player = new();

        IMinion minion = new AlexCard();
        IMinion minion2 = new AlexCard();

        // Initialize PlayerHand property
        PlayerHand = new ObservableCollection<IBaseCard>();

        // Add items to PlayerHand
        PlayerHand.Add(minion);
        PlayerHand.Add(minion2);
    }
}
