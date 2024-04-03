namespace CardCraftShared.Cards.Spells;

public class BelastingdienstSpell : BaseSpell
{
    public BelastingdienstSpell(): base(
        5,
        "Belastingdienst",
        "Demand 50% of the players mana",
        CardRarityEnum.RARE,
        "belastingdienstspell.jpeg"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero,
        BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}