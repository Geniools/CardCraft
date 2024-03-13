using CardCraftClient.Service;
using CardCraftShared.Cards.Minions;
using CardCraftShared.Cards.Spells;
using CardCraftShared.Core.Decorators;
using CardCraftShared.Core.Interfaces;
using System.Diagnostics;

namespace CardCraftShared;

public class GameManager
{
    private readonly SignalRService _signalRService;

    private  List<Player> Players { get; init; }
    private Board Board { get; init; }
    private Graveyard Graveyard { get; init; }

    public GameManager(SignalRService signalRService)
    {
        this._signalRService = signalRService;

        this.Players = new();
        this.Board = new();
        this.Graveyard = new();
    }
    
    public void StartGame()
    {
        this._signalRService.StartConnection();
        TestStuff();
    }

    public void EndGame()
    {
        this._signalRService.StopConnection();
    }

    public void AddPlayer()
    {

    }

    public void TestStuff() 
    {
        IMinion minion = new AlexCard();
        IMinion minion2 = new AlexCard();
        ResitSpell spell = new();
        DeckPool deck = new();
        deck.AddCard(minion);
        deck.AddCard(minion2);
        deck.AddCard(spell);
        deck.Shuffle();
        Trace.WriteLine(deck);
        minion = new DivineShield(minion);
        minion2 = new TauntDecorator(minion2);
        Trace.WriteLine(minion.Health);
        minion2.AttackMinion(minion);
        Trace.WriteLine(minion.Health);
        minion2.AttackMinion(minion);
        Trace.WriteLine(minion.Health);
    }
}
