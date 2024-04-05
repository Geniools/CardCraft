using CardCraftShared;
namespace CardCraftShared.Cards.Minions;

public class TerryCard : BaseMinion
{
    public TerryCard():base(
        3,
        3,
        5,
        "Terry",
        "50% chance of attacking a random character",
        CardRarityEnum.COMMON,
        "terrycard.jpg"
    ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }
}