namespace CardCraftShared.Cards.Spells;

public class AmogusSpell : BaseSpell
{
    public AmogusSpell() : base
        (
            4,
            "Amogus", 
            "Get control over an enemy minion",
            CardRarityEnum.LEGENDARY, 
            "amogusspell.jpg"
        ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}