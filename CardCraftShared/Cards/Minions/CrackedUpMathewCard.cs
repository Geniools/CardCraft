namespace CardCraftShared.Cards.Minions;

public class CrackedUpMathewCard : BaseMinion
{
    public CrackedUpMathewCard() : base(
        4,
        8,
        8,
        "Cracked Up Mathew",
        "Work finished in 0.3221 second.",
        CardRarityEnum.RARE,
        "crackedupmathewcard.jpg"
    ) { }
    public override void TriggerEffect()
    {
    }
}