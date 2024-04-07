namespace CardCraftShared.Cards.Spells;

public class ITRelationshipSpellUpgraded : BaseSpell
{
    public ITRelationshipSpellUpgraded() : base(
        6,
        "IT Relationship",
        "PEAK LOVE! \n Restore 5 Health. If player's Health is below or equal to 15, restore 10 instead",
        CardRarityEnum.RARE,
        "itrelationshipspellupgraded.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        if (player.Hero.Health <= 15)
        {
            player.Hero.Health += 10;
        }
        else
        {
            player.Hero.Health += 5;
        }
    }
}