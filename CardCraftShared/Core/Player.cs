namespace CardCraftShared;

public class Player(BaseHero hero)
{
    public BaseHero Hero { get; set; } = hero;
    private Deck Deck { get; set; }
    private Hand Hand { get; set; }

    public void PlayCard(BaseCard card)
    {
        throw new NotImplementedException();
    }
}
