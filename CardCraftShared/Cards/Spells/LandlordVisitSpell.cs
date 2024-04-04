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

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        throw new NotImplementedException();
    }
}