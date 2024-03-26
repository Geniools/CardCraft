using System.Diagnostics;
using CardCraftClient.Service;
using CardCraftShared;
using CardCraftShared.Cards.Heroes;
using CardCraftShared.Cards.Minions;
using CardCraftShared.Cards.Spells;
using CardCraftShared.Core.Decorators;
using CardCraftShared.Core.Interfaces;

namespace CardCraftClient.Model;

public class GameManager
{
    private readonly SignalRService _signalRService;

    public Player? Player1 { get; set; }
    public Player? Player2 { get; set; }
    private Board Board { get; init; }
    private Graveyard Graveyard { get; init; }

    public GameManager(SignalRService signalRService)
    {
        this._signalRService = signalRService;

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

    public void AddPlayer(Player player)
    {
        if (Player1 is null)
        {
            Player1 = player;
        }
        else Player2 ??= player;
    }

    public void TestStuff()
    {
        DeckPool deck = new();
        BaseHero hero = new AlexHero(1, "", "", "aboba");
        Player player = new();
        player.Hero = hero;
        player.Deck = deck;
        player.Hand = new Hand();
        player.Name = "Player";
        
        Board board = new();

        IMinion minion = new AlexCard();
        IMinion minion2 = new AlexCard();
        ResitSpell spell = new();

        deck.AddCard(minion);
        deck.AddCard(minion2);
        deck.AddCard(spell);
        deck.Shuffle();

        Player player1 = new();
        Player player2 = new();
        AddPlayer(player1);
        AddPlayer(player2);

        player1.DrawCard();
        player2.DrawCard();

        player1.PlayCard(minion, board);
        player2.PlayCard(minion, board);

        Trace.WriteLine(board.EnemySide.Count);
        Trace.WriteLine(board.FriendlySide.Count);

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
