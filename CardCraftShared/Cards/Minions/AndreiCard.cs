namespace CardCraftShared.Cards.Minions;

public class AndreiCard : BaseMinion
{
    public AndreiCard() : base(
        5, 
        5,
        4, 
        "Andrei",
        "Guiness Forever",
        CardRarityEnum.COMMON, 
        "andreicard.jpg"
        ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }
}