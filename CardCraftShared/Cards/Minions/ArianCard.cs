namespace CardCraftShared.Cards.Minions;

public class ArianCard : BaseMinion
{
    public ArianCard() : base(
        3,
        2,
        1,
        "Arian",
        "OI",
        CardRarityEnum.COMMON,
        "ariancard.jpg"
    ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }
}