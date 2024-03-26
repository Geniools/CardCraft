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

    public BaseHero Hero { get; set; }

    public DeckPool Deck { get; set; }

    public Hand Hand { get; set; }

    public IBaseCard PlayCard(IBaseCard card)
    {
        Hand.Remove(card);
        return card;
    }

    public void DrawCard()
    {
        if (Deck.IsEmpty()) return;

        IBaseCard card = Deck.DrawMinion();
        Hand.Add(card);
    }
}
