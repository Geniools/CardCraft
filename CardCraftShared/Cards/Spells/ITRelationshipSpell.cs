namespace CardCraftShared.Cards.Spells;

public class ITRelationshipSpell : BaseSpell
{
    public ITRelationshipSpell(): base(
        3,
        "IT Relationship",
        "OI",
        CardRarityEnum.COMMON,
        "itrelationshipspell.jpg"
    )
    { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        player.GiveHealth(2);
    }
}