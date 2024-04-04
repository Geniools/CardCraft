namespace CardCraftShared.Cards.Minions;

public class RobCard : BaseMinion
{
    public RobCard() : base(
        5,
        5,
        4,
        "Rob",
        "Car + Lamp = Love",
        CardRarityEnum.COMMON,
        "robcard.jpg"
    ) { }
    public override void TriggerEffect()
    {
    }
}