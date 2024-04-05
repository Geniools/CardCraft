namespace CardCraftShared.Cards.Minions;

public class NathanCard : BaseMinion
{
    public NathanCard(): base(
        3,
        3,
        2,
        "Nathan",
        "Its big brain time",
        CardRarityEnum.COMMON,
        "nathancard2.jpg"
    ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }
}