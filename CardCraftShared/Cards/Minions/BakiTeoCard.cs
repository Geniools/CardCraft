namespace CardCraftShared.Cards.Minions;

public class BakiTeoCard : BaseMinion
{
    public BakiTeoCard() : base(
        7,
        8,
        9,
        "Baki Teo",
        "Fight me father!",
        CardRarityEnum.RARE,
        "bakiteocard.jpg"
    ) { }
    public override void TriggerEffect()
    {
    }
}