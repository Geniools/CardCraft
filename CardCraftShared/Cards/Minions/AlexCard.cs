using CardCraftShared;
namespace CardCraftShared.Cards.Minions;

public class AlexCard : BaseMinion
{
    public AlexCard() : base(
        10,
        10,
        10,
        "Alex",
        "Solo army",
        CardRarityEnum.LEGENDARY,
        "alexcard.jpg"
    )
    { }
    public override void TriggerEffect()
    {
    }

}

