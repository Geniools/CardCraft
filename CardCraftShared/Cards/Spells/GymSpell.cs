namespace CardCraftShared.Cards.Spells;

public class GymSpell : BaseSpell
{
    public GymSpell() : base(
        3,
        "Gym",
        "Deal 2 damage to enemy Hero",
        CardRarityEnum.COMMON,
        "gymspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        enemyPlayer.Hero.TakeDamage(2);
    }
}