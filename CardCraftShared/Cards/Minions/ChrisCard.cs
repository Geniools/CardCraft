namespace CardCraftShared.Cards.Minions;

public class ChrisCard : BaseMinion
{
    public ChrisCard(): base(
        9,
        9,
        9,
        "Chris",
        "Fun? What is that?",
        CardRarityEnum.EPIC,
        "chriscard.jpg"
    ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }
}