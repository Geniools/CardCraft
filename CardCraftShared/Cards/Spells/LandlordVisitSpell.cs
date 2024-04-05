namespace CardCraftShared.Cards.Spells;

public class LandlordVisitSpell : BaseSpell
{
    public LandlordVisitSpell(): base(
        8,
        "Landlord visit",
        "Remove 6 Mana from enemy Hero",
        CardRarityEnum.EPIC,
        "landlordvisitspell.png"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        enemyPlayer.RemoveMana(6);
    }
}