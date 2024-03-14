namespace CardCraftShared;

public class Player
{
    public BaseHero Hero { get; set; }
    private DeckPool Deck { get; set; }
    private Hand Hand { get; set; }

    public Player(BaseHero hero, DeckPool deck)
    {
        this.Hero = hero;
        this.Deck = deck;
        this.Hand = new Hand();
    }

    public IBaseCard PlayCard(IBaseCard card)
    {
        Hand.Remove(card);
        return card;
    }

    public void DrawCard()
    {
        if (!Deck.IsEmpty())
        {
            IBaseCard card = Deck.DrawMinion();
            Hand.Add(card);
        }
    }
}
