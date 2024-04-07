namespace CardCraftShared.Cards.Spells;

public class GymSpell : BaseSpell
{
    public GymSpell() : base(
        2,
        "Gym",
        "Gain 3 health",
        CardRarityEnum.COMMON,
        "gymspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        player.Hero.Health += 3;
    }
}