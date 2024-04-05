namespace CardCraftShared.Cards.Minions;

public class MathewCard : BaseMinion
{
    public MathewCard(): base(
        3,
        5,
        6,
        "Mathew",
        "Biertje?",
        CardRarityEnum.COMMON,
        "mathewcard.jpg"
    ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }
}