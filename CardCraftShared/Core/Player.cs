namespace CardCraftShared;

public class Player(BaseHero hero)
{
    public BaseHero Hero { get; set; } = hero;
    private DeckPool Deck { get; set; }
    private Hand Hand { get; set; }

    public void PlayCard(IBaseCard card)
    {
        throw new NotImplementedException();
    }
}
