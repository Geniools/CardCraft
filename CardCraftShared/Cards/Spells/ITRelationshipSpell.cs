namespace CardCraftShared.Cards.Spells;

public class ITRelationshipSpell : BaseSpell
{
    public ITRelationshipSpell(): base(
        8,
        "IT Relationship",
        "LGBTQ",
        CardRarityEnum.LEGENDARY,
        "itrelationshipspell.jpg"
    )
    { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero,
        BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}