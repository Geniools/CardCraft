namespace CardCraftShared.Cards.Spells;

public class CorridorCoffeeSpell : BaseSpell
{
    public CorridorCoffeeSpell() : base(
        3,
        "Corridor Coffee",
        "Sanity Restored",
        CardRarityEnum.COMMON,
        "corridorcoffeespell.jpg"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}