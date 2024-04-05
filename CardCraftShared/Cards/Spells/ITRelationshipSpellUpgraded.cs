namespace CardCraftShared.Cards.Spells;

public class ITRelationshipSpellUpgraded : BaseSpell
{
    public ITRelationshipSpellUpgraded() : base(
        6,
        "IT Relationship",
        "Give 4 Health to Hero",
        CardRarityEnum.RARE,
        "itrelationshipspellupgraded.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        player.GiveHealth(4);
    }
}