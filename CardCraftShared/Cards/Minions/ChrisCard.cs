namespace CardCraftShared.Cards.Minions;

public class ChrisCard : BaseMinion
{
    public ChrisCard(): base(
        9,
        9,
        9,
        "Chris",
        "Removes one card from the enemy board that is less than 5 mana",
        CardRarityEnum.EPIC,
        "chriscard.jpg"
    ) { }
}