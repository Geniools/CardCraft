namespace CardCraftShared.Cards.Spells;

public class DutchHousing : BaseSpell
{
    public DutchHousing(): base(
        4,
        "Dutch housing",
        "No sleep for you, hero takes 2 damage",
        CardRarityEnum.COMMON,
        "rentedroomspell.jpg"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}