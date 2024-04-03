namespace CardCraftShared.Cards.Spells;

public class LandlordVisitSpell : BaseSpell
{
    public LandlordVisitSpell(): base(
        8,
        "Landlord visit",
        "Sorry, but you need to pay 10.000$ for your gas bill",
        CardRarityEnum.EPIC,
        "landlordvisitspell.png"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}