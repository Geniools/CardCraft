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

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        enemyPlayer.Hero.TakeDamage(4);
    }
}