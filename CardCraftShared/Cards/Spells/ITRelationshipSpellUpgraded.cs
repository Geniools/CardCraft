namespace CardCraftShared.Cards.Spells;

public class ITRelationshipSpellUpgraded : BaseSpell
{
    public ITRelationshipSpellUpgraded() : base(
        6,
        "IT Relationship",
        "OI",
        CardRarityEnum.RARE,
        "itrelationshipspellupgraded.jpg"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero,
        BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}