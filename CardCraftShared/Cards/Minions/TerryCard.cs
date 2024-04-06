using CardCraftShared;
namespace CardCraftShared.Cards.Minions;

public class TerryCard : BaseMinion
{
    public TerryCard():base(
        3,
        3,
        5,
        "Terry",
        "Has a 1/10 chance of showing up to university",
        CardRarityEnum.COMMON,
        "terrycard.jpg"
    ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }
}