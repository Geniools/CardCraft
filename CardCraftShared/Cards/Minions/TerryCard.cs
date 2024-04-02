using CardCraftShared;
namespace CardCraftShared.Cards.Minions;

public class TerryCard : BaseMinion
{
    public TerryCard():base(
        3,
        3,
        6,
        "Terry",
        "50% chance of attacking a random character",
        CardRarityEnum.RARE,
        "terrycard.jpg"
    ) { }
}