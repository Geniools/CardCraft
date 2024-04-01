using CardCraftShared;
namespace CardCraftShared.Cards.Minions;

public class AlexCard : BaseMinion
{
    public AlexCard() : base(
        8,
        8,
        8,
        "Alex", 
        "Alex is a strong minion",
        CardRarityEnum.LEGENDARY, 
        "alexcard.jpg"
        ) { }
}

