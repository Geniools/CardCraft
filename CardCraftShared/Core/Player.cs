namespace CardCraftShared;

public class Player : ICloneable
{
    public const int MAX_MANA = 10;

    public Player()
    {
        this.Hand = new Hand();
        this.Deck = new DeckPool();
        this.PlayerSignalRDetails = new PlayerSignalRDetails();
        this.Mana = 1;
    }

    // Connection-related properties

    public string? ConnectionId { get; set; }

    public string Name { get; set; }

    // This property contains details that need to be sent via SignalR
    // Because of SignalR limitations,  the whole Player object cannot be sent
    public PlayerSignalRDetails PlayerSignalRDetails { get; set; }

    // Game-related properties

    public BaseHero? Hero { get; set; }

    public DeckPool Deck { get; set; }

    public Hand Hand { get; set; }
    public int Mana { get; set; }

    public IBaseCard PlayCard(IBaseCard card)
    {
        return this.Hand.Remove(card);
    }

    public void DrawCard()
    {
        if (!Deck.IsEmpty())
        {
            IBaseCard card = Deck.DrawCard();
            Hand.Add(card);
        }
    }

    public void IncreaseMana()
    {
        if (this.Mana < MAX_MANA)
        {
            this.Mana++;
        }
    }

    public object Clone()
    {
        return new Player
        {
            ConnectionId = this.ConnectionId,
            Name = this.Name,
            PlayerSignalRDetails = this.PlayerSignalRDetails,
            Hero = this.Hero,
            Deck = this.Deck,
            Hand = this.Hand,
            Mana = this.Mana
        };
    }
}
