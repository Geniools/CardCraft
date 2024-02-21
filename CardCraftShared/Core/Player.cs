namespace CardCraftShared;

public class Player
{
    public BaseHero Hero { get; set; }
    private Deck Deck { get; set; }
    private Hand Hand { get; set; }

    public Player(BaseHero hero)
    {
        this.Hero = hero;
    }

    public void PlayCard(BaseCard card)
    {
        throw new NotImplementedException();
    }
}
