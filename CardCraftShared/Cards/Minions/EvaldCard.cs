namespace CardCraftShared.Cards.Minions;

public class EvaldCard : BaseMinion
{
    public EvaldCard() : base(
        4, 
        3,
        4, 
        "Evald",
        "Evald is a very powerful card",
        CardRarityEnum.COMMON, 
        "evaldcard.jpg"
        ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }
}