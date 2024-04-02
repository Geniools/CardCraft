namespace CardCraftShared.Cards.Spells;

public class AlcoholismSpell : BaseSpell
{
    public AlcoholismSpell() : base(
        2,
        "Alcoholism",
        "Casual Tuesday, gain 2 health",
        CardRarityEnum.COMMON,
        "alcoholismspell.jpg"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}