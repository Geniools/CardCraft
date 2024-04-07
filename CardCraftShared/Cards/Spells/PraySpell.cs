namespace CardCraftShared.Cards.Spells;

public class PraySpell : BaseSpell
{
    public PraySpell(): base(
        1,
        "Pray",
        "I hope this works this time \n Gain 1 Health",
        CardRarityEnum.COMMON,
        "prayspell.png"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        player.GiveHealth(1);
    }
}