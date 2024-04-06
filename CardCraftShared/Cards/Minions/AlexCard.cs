using CardCraftShared;
namespace CardCraftShared.Cards.Minions;

public class AlexCard : BaseMinion
{
    public AlexCard() : base(
        10,
        10,
        10,
        "Alex",
        "Reads .NET documentation in his free time",
        CardRarityEnum.LEGENDARY,
        "alexcard.jpg"
    )
    { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }

}

