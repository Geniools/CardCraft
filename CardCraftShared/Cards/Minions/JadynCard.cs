namespace CardCraftShared.Cards.Minions;

public class JadynCard : BaseMinion
{
    public JadynCard() : base(
        4,
        4,
        5,
        "Jadyn",
        "Ticklish",
        CardRarityEnum.COMMON,
        "jadyncard.jpg"
    ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }
}