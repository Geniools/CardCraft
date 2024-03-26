using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CardCraftShared.Cards.Heroes;
using CardCraftShared.Cards.Minions;
using CardCraftShared.Core.Interfaces;

namespace CardCraftClient.ViewModel;

public partial class GamePageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _username;    
    
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

    public GamePageViewModel()
    {
        this.Title = "Game";
        this.Username = "Chris";
        this.Attack = 5;
        this.Health = 10;
        this.ManaCost = 3;
        this.Description = "nice";
        this.Id = 1;
        DeckPool deck = new();
        BaseHero hero = new AlexHero(1, "", "", "aboba");
        Player player = new(hero, deck);

        IMinion minion = new AlexCard();
        IMinion minion2 = new AlexCard();

        // Initialize PlayerHand property
        PlayerHand = new ObservableCollection<IBaseCard>();

        // Add items to PlayerHand
        PlayerHand.Add(minion);
        PlayerHand.Add(minion2);
    }
}
