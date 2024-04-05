namespace CardCraftShared.Cards.Minions;

public class DimitriCard : BaseMinion
{
    public DimitriCard() : base(
        5,
        5,
        7,
        "Dimitri",
        "Guesses your location 2 seconds",
        CardRarityEnum.RARE,
        "dimitricard.jpg"
    ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }
}