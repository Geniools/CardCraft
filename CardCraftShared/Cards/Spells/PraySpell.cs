namespace CardCraftShared.Cards.Spells;

public class PraySpell : BaseSpell
{
    public PraySpell(): base(
        2,
        "Pray",
        "Add 1 health point to Player/Hero",
        CardRarityEnum.COMMON,
        "prayspell.png"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        player.GiveHealth(1);
    }
}