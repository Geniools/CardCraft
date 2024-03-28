namespace CardCraftShared;

public class Player
{
    public Player() { }

    public Player(string name)
    {
        this.Hand = new Hand();
        this.Name = name;
    }

    // Connection-related properties

    public string? ConnectionId { get; set; }

    public string Name { get; set; }

    // Game-related properties

    public BaseHero? Hero { get; set; }

    public DeckPool Deck { get; set; }

    public Hand Hand { get; set; }

    public void PlayCard(IBaseCard card, Board board)
    {
        Hand.Remove(card);
        board.AddCard(card, this);
    }

    public void DrawCard()
    {
        if (!Deck.IsEmpty())
        {
            IBaseCard card = Deck.DrawCard();
            Hand.Add(card);
        }
    }
}
