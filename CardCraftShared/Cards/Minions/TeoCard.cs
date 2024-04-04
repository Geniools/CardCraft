namespace CardCraftShared.Cards.Minions;

public class TeoCard : BaseMinion
{
    public TeoCard() : base(
        5, 
        5,
        5, 
        "Teo",
        "Teo is a very powerful card",
        CardRarityEnum.COMMON, 
        "teocard2.jpg"
        ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }
}