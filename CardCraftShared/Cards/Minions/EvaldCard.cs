namespace CardCraftShared.Cards.Minions;

public class EvaldCard : BaseMinion
{
    public EvaldCard() : base(
        4, 
        3,
        4, 
        "Evald",
        "League of Legends Zoe main",
        CardRarityEnum.COMMON, 
        "evaldcard.jpg"
        ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }
}