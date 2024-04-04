namespace CardCraftShared.Cards.Spells;

public class GymSpell : BaseSpell
{
    public GymSpell() : base(
        3,
        "Gym",
        "What am i doing today? Chest and back",
        CardRarityEnum.COMMON,
        "gymspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        enemyPlayer.Hero.TakeDamage(2);
    }
}